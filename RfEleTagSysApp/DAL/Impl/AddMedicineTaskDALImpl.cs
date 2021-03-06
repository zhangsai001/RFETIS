﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RfEleTagSysApp.Model;
using RfEleTagSysApp.HAL;
using MySql.Data.MySqlClient;
using System.Data;

namespace RfEleTagSysApp.DAL.Impl
{
    class AddMedicineTaskDALImpl : AddMedicineTaskDAL
    {
        private UserDAL userDAL = new UserDALImpl();

        public bool create(AddMedicineTask task)
        {
            return SqlHelper.ExecuteNonQuery("insert into AddMedicineTask(Name,Items_All,Items_Complete,Manager,Operator,State) values(@name,@items_all,@items_complete,@manager,@operator,@state)",
                new MySqlParameter[] {
                    new MySqlParameter("@name",task.Name),
                    new MySqlParameter("@items_all",task.Items_All),
                    new MySqlParameter("@items_complete",task.Items_Complete),
                    new MySqlParameter("@manager", task.Manager.Id),
                    new MySqlParameter("@operator",task.Operator.Id),
                    new MySqlParameter("@state", task.State)
                }) > 0;
        }

        public bool delete(int taskId)
        {
            return SqlHelper.ExecuteNonQuery("delete from AddMedicineTask where Id=@id", new MySqlParameter[] {
                new MySqlParameter("@id",taskId)
            }) > 0;
        }

        public List<AddMedicineTask> findListByOperatorId(int operatorId)
        {
            List<AddMedicineTask> list = new List<AddMedicineTask>();
            DataTable table = SqlHelper.ExecuteDataTable("select * from AddMedicineTask where Operator=@operatorId",
                new MySqlParameter[] {
                    new MySqlParameter("@operatorId", operatorId)
                });
            foreach (DataRow row in table.Rows)
            {
                AddMedicineTask task = new AddMedicineTask();
                task.Id = (int)row[0];
                task.Name = (string)row[1];
                task.Items_All = (int)row[2];
                task.Items_Complete = (int)row[3];
                task.Manager = userDAL.findUserById((int)row[4]);
                task.Operator = userDAL.findUserById((int)row[5]);
                task.State = (string)row[6];
                list.Add(task);
            }
            return list;
        }

        public AddMedicineTask findTaskById(int taskId)
        {
            AddMedicineTask task = null;
            DataTable table = SqlHelper.ExecuteDataTable("select * from AddMedicineTask where Id=@id", new MySqlParameter[] {
                new MySqlParameter("@id",taskId)
            });
            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                task = new AddMedicineTask();
                task.Id = (int)row[0];
                task.Name = (string)row[1];
                task.Items_All = (int)row[2];
                task.Items_Complete = (int)row[3];
                task.Manager = userDAL.findUserById((int)row[4]);
                task.Operator = userDAL.findUserById((int)row[5]);
                task.State = (string)row[6];
            }
            return task;
        }

        public List<AddMedicineTask> list()
        {
            List<AddMedicineTask> list = new List<AddMedicineTask>();
            DataTable table = SqlHelper.ExecuteDataTable("select * from AddMedicineTask");
            foreach (DataRow row in table.Rows)
            {
                AddMedicineTask task = new AddMedicineTask();
                task.Id = (int)row[0];
                task.Name = (string)row[1];
                task.Items_All = (int)row[2];
                task.Items_Complete = (int)row[3];
                task.Manager = userDAL.findUserById((int)row[4]);
                task.Operator = userDAL.findUserById((int)row[5]);
                task.State = (string)row[6];
                list.Add(task);
            }
            return list;
        }

        public bool update(AddMedicineTask task)
        {
            return SqlHelper.ExecuteNonQuery("update AddMedicineTask set Name=@name,Items_All=@items_all,Items_Complete=@items_complete,Manager=@manager,Operator=@operator,State=@state where Id=@id",
                new MySqlParameter[] {
                    new MySqlParameter("@name",task.Name),
                    new MySqlParameter("@items_all",task.Items_All),
                    new MySqlParameter("@items_complete",task.Items_Complete),
                    new MySqlParameter("@manager", task.Manager.Id),
                    new MySqlParameter("@operator",task.Operator.Id),
                    new MySqlParameter("@state", task.State),
                    new MySqlParameter("@id",task.Id)
                }) > 0;
        }
    }
}
