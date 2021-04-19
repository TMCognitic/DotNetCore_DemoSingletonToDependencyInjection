using InversionOfControl.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace InversionOfControl.Models.Mappers
{
    internal static class DataRecordExtensions
    {
        internal static Person ToPerson(this IDataRecord record)
        {
            return new Person()
            {
                BusinessEntityId = (int)record["BusinessEntityId"],
                Title = (record["Title"] is DBNull) ? null : (string)record["Title"],
                LastName = (string)record["LastName"],
                FirstName = (string)record["FirstName"]
            };
        }
    }
}
