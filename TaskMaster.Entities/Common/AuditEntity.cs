﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMaster.Entities.Common
{
    public class AuditEntity
    {
        [Column("is_active")]
        public bool? IsActive
        {
            get; set;
        }

        [Column("is_deleted")]
        public bool? IsDeleted
        {
            get; set;
        }

        [Column("creation_date")]
        public DateTimeOffset? CreationDate
        {
            get; set;
        }

        [Column("create_by_user")]
        public Guid? CreateByUser
        {
            get; set;
        }

        [Column("modification_date")]
        public DateTimeOffset? ModificationDate
        {
            get; set;
        }

        [Column("update_by_user")]
        public Guid? UpdateByUser
        {
            get; set;
        }

        public void Delete()
        {
            ModificationDate = DateTime.Now;
            IsActive = false;
            IsDeleted = true;
        }
    }

    public class AuditEntityWithID
    {
        [Column("id")]
        public Guid Id
        {
            get; set;
        }

        [Column("is_active")]
        public bool? IsActive
        {
            get; set;
        }

        [Column("is_deleted")]
        public bool? IsDeleted
        {
            get; set;
        }

        [Column("creation_date")]
        public DateTimeOffset? CreationDate
        {
            get; set;
        }

        [Column("create_by_user")]
        public Guid? CreateByUser
        {
            get; set;
        }

        [Column("modification_date")]
        public DateTimeOffset? ModificationDate
        {
            get; set;
        }

        [Column("update_by_user")]
        public Guid? UpdateByUser
        {
            get; set;
        }

        public void Delete()
        {
            ModificationDate = DateTime.Now;
            IsActive = false;
            IsDeleted = true;
        }
    }

    public class AuditEntityNotUserFields
    {
        [Column("is_active")]
        public bool? IsActive
        {
            get; set;
        }

        [Column("is_deleted")]
        public bool? IsDeleted
        {
            get; set;
        }

        [Column("creation_date")]
        public DateTimeOffset? CreationDate
        {
            get; set;
        }

        public void Delete()
        {
            IsActive = false;
            IsDeleted = true;
        }
    }
}