using IdGen;
using LinqKit;
using NAutowired.Core.Attributes;
using SQLite;
using SubscribeManagement.WebAPI.DA.Entities;
using SubscribeManagement.WebAPI.Exceptions;
using SubscribeManagement.WebAPI.Extensions;
using SubscribeManagement.WebAPI.Models;
using SubscribeManagement.WebAPI.Models.Connection;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace SubscribeManagement.WebAPI.Services
{
    [Service(NAutowired.Core.Models.Lifetime.Scoped)]
    public class ConnectionService
    {

        private readonly SQLiteConnection _connection;
        private readonly IdGenerator _idWorker;
        private readonly AutoMapper.IMapper _mapper;

        public ConnectionService(SQLiteConnection connection, IdGenerator idWorker, AutoMapper.IMapper autoMapper)
        {
            _connection = connection;
            _idWorker = idWorker;
            _mapper = autoMapper;
        }

        public async Task<PageCollection<Connection>> PageSearch(PageSearchModel<SearchConnectionModel> pageSearchModel)
        {
            var filterExpression = PredicateBuilder.New<Connection>(true);

            if (pageSearchModel?.Search?.CreateAtBegin.HasValue ?? false)
            {
                filterExpression.And(c => c.CreateAt >= pageSearchModel.Search.CreateAtBegin.Value);
            }

            if (pageSearchModel?.Search?.CreateAtEnd.HasValue ?? false)
            {
                filterExpression.And(c => c.CreateAt <= pageSearchModel.Search.CreateAtEnd.Value);
            }

            if (pageSearchModel?.Search?.Id.HasValue ?? false)
            {
                filterExpression.And(c => c.Id == pageSearchModel.Search.Id);
            }
            if (!string.IsNullOrWhiteSpace(pageSearchModel?.Search?.Name))
            {
                filterExpression.And(c => c.Name.Contains(pageSearchModel.Search.Name));
            }

            var pagedCollection = _connection.Table<Connection>().Where(filterExpression).Skip(pageSearchModel.SkipCount).Take(pageSearchModel.PageSize).ToList();
            var totalSize = _connection.Table<Connection>().Where(filterExpression).Count();
            return pageSearchModel.Done(totalSize, pagedCollection);
        }

        public async Task Create(CreateConnectionModel createModel)
        {
            //TODO: 校验

            //转换为模型存储
            Connection conn = _mapper.Map<Connection>(createModel);
            conn.Id = _idWorker.CreateId();
            conn.CreateAt = DateTime.Now;
            conn.UpdateAt = DateTime.Now;

            //ExtraProperties
            var connExtraProperties = createModel.ExtraProperties.Select(c => new ConnectionExtraProperty
            {
                ConnectionId = conn.Id,
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                Id = _idWorker.CreateId(),
                Key = c.Key,
                Value = c.Value
            });
            using (TransactionScope trans = new TransactionScope())
            {
                _connection.Insert(conn);
                _connection.InsertAll(connExtraProperties);
                trans.Complete();
            }
        }

        public async Task<string> GetURI(long id)
        {
            var connection = _connection.Get<Connection>(c => c.Id == id);
            var connectionExtraProperties = _connection.Table<ConnectionExtraProperty>().Where(c => c.ConnectionId == id).ToList();

            IDictionary<string, object> data = new ExpandoObject();
            foreach (var propertyInfo in connection.GetType().GetProperties())
            {
                data.Add(propertyInfo.Name, propertyInfo.GetValue(connection));
            }

            foreach (var extraProperty in connectionExtraProperties)
            {
                if (data.ContainsKey(extraProperty.Key))
                {
                    data[extraProperty.Key] = extraProperty.Value;
                    continue;
                }
                data.Add(extraProperty.Key, extraProperty.Value);
            }

            var parseRule = _connection.Get<ProtocolParseRule>(c => c.Code == connection.ProtocolCode);

            var (success, result) = parseRule.ParseScript.Evaluate(data);
            if (success)
                return result;
            throw new BusinessException(result);
        }
    }
}
