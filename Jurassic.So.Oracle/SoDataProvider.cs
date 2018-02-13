using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using Jurassic.AppCenter;
using Jurassic.Com.Tools;
using Jurassic.CommonModels;
using Jurassic.CommonModels.EntityBase;

namespace Jurassic.So.Oracle
{
    /// <summary>
    /// 对EFAuditDataService的扩展，对实现ICUDEntity接口的实体类记录修改前后的值
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    public class SoDataProvider<T> : EFAuditDataService<T> where T : class
    {
       // IDataLogService _dataLogService;

        public SoDataProvider(SooilDbContext context//, IDataLogService dataLogService
            )
            : base(context)
        {
            _context = context;
            //_dataLogService = dataLogService;
        }

        public override IQueryable<T> GetQuery()
        {
            if (typeof(ICanLogicalDeleteEntity).IsAssignableFrom(typeof(T)))
            {
                return base.GetQuery().Where("IsDeleted=False");
            }
            return base.GetQuery();
        }

        public override int Add(T t)
        {
            ICUEntity newEntity = t as ICUEntity;
            if (newEntity != null)
            {
                if (newEntity.CreaterId == 0)
                {
                    newEntity.CreaterId = AppManager.Instance.GetCurrentUserId().ToInt();
                }

                if (newEntity.CreateTime == default(DateTime))
                {
                    newEntity.CreateTime = DateTime.Now;
                }
                if (newEntity.UpdaterId == 0)
                {
                    newEntity.UpdaterId = AppManager.Instance.GetCurrentUserId().ToInt();
                }
                if (newEntity.UpdateTime == default(DateTime))
                {
                    newEntity.UpdateTime = DateTime.Now;
                }
            }
            ICanLogicalDeleteEntity canLogicalDeleteEntity = t as ICanLogicalDeleteEntity;
            if (canLogicalDeleteEntity != null)
                canLogicalDeleteEntity.IsDeleted = false;

            //var billEntity = t as BillEntity;
            //if (billEntity != null)
            //{
            //    if (billEntity.DeptId == 0)
            //    {
            //        billEntity.DeptId = EPASUserContext.Instance.DeptId.Value;
            //    }

            //    if (billEntity.InvOrgId == 0)
            //    {
            //        billEntity.InvOrgId = EPASUserContext.Instance.InvOrgId.Value;
            //    }
            //}
            //var otherBillEntity = t as OtherBillEntity;
            //if (otherBillEntity != null)
            //{
            //    if (otherBillEntity.DeptId == 0)
            //    {
            //        otherBillEntity.DeptId = EPASUserContext.Instance.DeptId.Value;
            //    }
            //}
            ////添加数据前将Code赋值
            //bool needGenerateSeq = false;
            //ICodedEntity codeEntity = t as ICodedEntity;
            //if (codeEntity != null && string.IsNullOrWhiteSpace(codeEntity.Code))
            //{
            //    var codeRuleService = SiteManager.Get<ICodeRuleService>();
            //    var metadataInfo = SiteManager.Get<MetadataManager>().GetMetadata<T>();
            //    if (metadataInfo != null && !string.IsNullOrWhiteSpace(metadataInfo.Code))
            //    {
            //        needGenerateSeq = true;
            //        codeEntity.Code = codeRuleService.GenerateCode(metadataInfo.Code);
            //    }

            //}
            //var r = base.Add(t);
            ////添加数据后将流水号更新
            //if (needGenerateSeq)
            //{
            //    var metadataInfo = SiteManager.Get<MetadataManager>().GetMetadata<T>();
            //    var codeRuleService = SiteManager.Get<ICodeRuleService>();
            //    var lastSeq = codeRuleService.GetLastSeq(metadataInfo.Code);
            //    codeRuleService.UpdateSequence(metadataInfo.Code, lastSeq);
            //}

            //_dataLogService.Add(new SavedObject
            //{
            //    NewEntity = newEntity,
            //    OpType = DataOpType.Create,
            //    OpTime = DateTime.Now,
            //    UserId = AppManager.Instance.GetCurrentUserId().ToInt(),
            //});

            var r = base.Add(t);
            return r;
        }

        /// <summary>
        /// 修改操作
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override int Change(T t)
        {
            IIdEntity oldEntity = null;
            IIdEntity idEntity = t as IIdEntity;
            if (idEntity != null)
            {
                using (var newContext = SiteManager.Get<SooilDbContext>())
                {
                    oldEntity = newContext.Set<T>().Find(idEntity.Id) as IIdEntity;
                }
            }

            ICUEntity newEntity = t as ICUEntity;
            if (newEntity != null)
            {
                if (newEntity.CreaterId == 0)
                {
                    newEntity.CreaterId = AppManager.Instance.GetCurrentUserId().ToInt();
                }

                if (newEntity.CreateTime == default(DateTime))
                {
                    newEntity.CreateTime = ((ICUEntity)oldEntity).CreateTime;
                }
                if (newEntity.UpdaterId == 0)
                {
                    newEntity.UpdaterId = AppManager.Instance.GetCurrentUserId().ToInt();
                }
                if (newEntity.UpdateTime == default(DateTime))
                {
                    newEntity.UpdateTime = DateTime.Now;
                }
            }
            var r = base.Change(t);

            //_dataLogService.Add(new SavedObject
            //{
            //    OldEntity = oldEntity,
            //    NewEntity = newEntity,
            //    OpType = DataOpType.Update,
            //    OpTime = DateTime.Now,
            //    UserId = AppManager.Instance.GetCurrentUserId().ToInt(),
            //});

            return r;
        }

        public List<T> GetByExpression(Expression<Func<T, bool>> filterExpression)
        {
            if (filterExpression == null)
                return GetQuery().ToList();
            return GetQuery().Where(filterExpression).ToList();
        }

        public List<T> GetByExpr(string filterExpr)
        {
            if (string.IsNullOrWhiteSpace(filterExpr))
                return GetQuery().ToList();
            var filterExpression = Jurassic.Com.Tools.DynamicExpression.ParseLambda<T, bool>(filterExpr);
            return GetQuery().Where(filterExpression).ToList();
        }

        public override T GetByKey(object key)
        {
            if (CommOp.CanConvertTo<T, IIdEntity>())
            {
                return _context.Set<T>().Where("Id=" + key).FirstOrDefault();
            }
            return base.GetByKey(key);
        }

        /// <summary>
        /// 删除，只有当类实现了ICUDEntity时，才是逻辑删除
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override int Delete(T t)
        {
            ICanLogicalDeleteEntity ldEntity = t as ICanLogicalDeleteEntity;
            if (ldEntity != null)
            {
                ldEntity.IsDeleted = true;
            }
            else
            {
                _context.Set<T>().Remove(t);

            }
            //_dataLogService.Add(new SavedObject
            //{
            //    NewEntity = ldEntity,
            //    OpType = ldEntity == null ? DataOpType.PhysicalDelete : DataOpType.LogicalDelete,
            //    OpTime = DateTime.Now,
            //    UserId = AppManager.Instance.GetCurrentUserId().ToInt(),
            //});
            var r = _context.SaveChanges();
            return r;
        }

        public virtual int UpdatePartialFields(T model, params Expression<Func<T, object>>[] propertiesToUpdate)
        {
            try
            {
                _context.Set<T>().Attach(model);
                propertiesToUpdate.ToList().ForEach(p => _context.Entry(model).Property(p).IsModified = true);
                return _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {

                throw;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public virtual int UpdatePartialFields(List<T> models, params Expression<Func<T, object>>[] propertiesToUpdate)
        {
            try
            {
                List<Expression<Func<T, object>>> fieldUpdateExpressions = propertiesToUpdate.ToList();
                foreach (var model in models)
                {
                    _context.Set<T>().Attach(model);
                    fieldUpdateExpressions.ForEach(p => _context.Entry(model).Property(p).IsModified = true);
                }
                return _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {

                throw;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public virtual IQueryable<TResult> QueryPartialFields<TResult>(Expression<Func<T, bool>> filterExpression,
            Expression<Func<T, TResult>> expression)
        {
            if (filterExpression == null)
                return _context.Set<T>().Select(expression);
            return _context.Set<T>().Where(filterExpression).Select(expression);
        }

        //public virtual IQueryable<TResult> QueryPartialFieldsFromExp<TResult>(string filterExp,string selectExp)
        //{
        //    //filterExp: SourceModuleCode="Code" and SourceBillId=1
        //    var filterExpression = Jurassic.Com.Tools.DynamicExpression.ParseLambda<T, bool>(filterExp);

        //    //DynamicExpression.ParseLambda(typeof(T), typeof(bool),"Id = @0 and Orders.Count >= @1", "1", 10);

        //    var selectExpression=Jurassic.Com.Tools.DynamicExpression.ParseLambda<T,TResult>(selectExp);
        //    return QueryPartialFields(filterExpression, selectExpression);
        //}
    }
}


