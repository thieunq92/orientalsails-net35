using System;
using System.Collections;
using CMS.Core.Domain;
using NHibernate;
using NHibernate.Criterion;

namespace CMS.Core.DataAccess
{
    /// <summary>
    /// Functionality for common simple data access.
    /// </summary>
    public interface ICommonDao
    {
        /// <summary>
        /// Get a single instance from the database by type and primary key.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        object GetObjectById(Type type, int id);

        /// <summary>
        /// Get a single instance from the database by type and primary key. Optionally indicate if the
        /// object may be null when it is not found.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <param name="allowNull"></param>
        /// <returns></returns>
        object GetObjectById(Type type, int id, bool allowNull);

        /// <summary>
        /// Get a single instance from the database by type and a string description of a given property.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyName"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        object GetObjectByDescription(Type type, string propertyName, string description);

        IList GetObjectByProperty(Type type, string propertyName, object value);

        /// <summary>
        /// Get all objects of a given type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IList GetAll(Type type);

        /// <summary>
        /// Get all objects of a given type sorted on the given properties (ascending).
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sortProperties"></param>
        /// <returns></returns>
        IList GetAll(Type type, params string[] sortProperties);

        /// <summary>
        /// Save or update a given object in the database.
        /// </summary>
        /// <param name="obj"></param>
        void SaveOrUpdateObject(object obj);

        /// <summary>
        /// Explicit update
        /// </summary>
        /// <param name="obj"></param>
        void UpdateObject(object obj);

        /// <summary>
        /// Explicit save
        /// </summary>
        /// <param name="obj"></param>
        void SaveObject(object obj);

        /// <summary>
        /// Delete a given object from the database;
        /// </summary>
        /// <param name="obj"></param>
        void DeleteObject(object obj);

        /// <summary>
        /// Mark a given object for deletion but don't delete it (yet) from the database.
        /// </summary>
        /// <param name="obj"></param>
        void MarkForDeletion(object obj);

        int Count(Type type);

        /// <summary>
        /// Get list of object use criterion, if the criteria is a failure, return null
        /// </summary>
        /// <param name="objectType"></param>
        /// <param name="criterion"></param>
        /// <param name="orders"></param>
        /// <returns></returns>
        IList GetObjectByCriterion(Type objectType, ICriterion criterion, params Order[] orders);

        IList GetObjectByCriterionPaged(Type objectType, ICriterion criterion, int pageIndex,
                                        int pageSize, params Order[] orders);
        int CountObjectByCriterion(Type objectType, ICriterion criterion);

        /// <summary>
        /// Lấy tất cả quyền thuộc về module đối với role
        /// </summary>
        /// <param name="module"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        IList PermissionsGetByRole(ModuleType module, Role role);

        /// <summary>
        /// Lấy về record quyền name của role
        /// </summary>
        /// <param name="name"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        SpecialPermission PermissionGetByRole(string name, Role role);

        SpecialPermission PermissionGetByUser(string name, User user);

        /// <summary>
        /// Lấy tất cả quyền của user theo các role của nó
        /// </summary>
        /// <param name="module"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        IList PermissionsGetByUserRole(ModuleType module, User user);

        /// <summary>
        /// Lấy tất của quyền của user (không theo role)
        /// </summary>
        /// <param name="module"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        IList PermissionsGetByUser(ModuleType module, User user);

        /// <summary>
        /// Kiểm tra xem user có quyền name không (luôn trả về true nếu quyền đó không tồn tại)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        bool PermissionCheck(string name, User user);

        ISession OpenSession();

        #region -- NEW WAY TO ACCESS --
        /// <summary>
        /// Thực thi một thao tác đối với đối tượng get về (yêu cầu khai báo rõ loại đối tượng)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="criterion"></param>
        /// <param name="handler"></param>
        void ExecuteObject<T>(ICriterion criterion, ExecuteHandler<T> handler);
        #endregion

    }

    public delegate void ExecuteHandler<in T>(T obj);
}