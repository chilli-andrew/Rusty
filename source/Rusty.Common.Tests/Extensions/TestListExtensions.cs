using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rusty.Common.Extensions;
using System.Data;

namespace Rusty.Common.Tests.Extensions
{
    [TestFixture]
    public class TestListExtensions
    {
        [Test]
        public void AsDataTable_GivenEmptyList_ShouldReturnEmptyDataTable()
        { 
            // Arrange
            var users = new List<User>();
            // Act
            var dt = users.AsDataTable<User>();
            // Assert
            Assert.IsNotNull(dt);
            Assert.AreEqual(0, dt.Rows.Count);
        }

        [Test]
        public void AsDataTable_GivenEmptyList_ShouldReturnEmptyDataTableWithColumns()
        {
            // Arrange
            var users = new List<User>();
            // Act
            var dt = users.AsDataTable<User>();
            // Assert
            Assert.AreEqual(3, dt.Columns.Count);
            Assert.AreEqual("FirstName", dt.Columns[0].ColumnName);
            Assert.AreEqual("LastName", dt.Columns[1].ColumnName);
            Assert.AreEqual("Age", dt.Columns[2].ColumnName);
        }

        [Test]
        public void AsDataTable_GivenOneItemList_ShouldReturnDataTable()
        {
            // Arrange
            var users = new List<User> 
            { 
                new User { FirstName = "Bob", LastName="Smith", Age=25},
            };

            // Act
            var dt = users.AsDataTable<User>();
            // Assert
            Assert.AreEqual(1, dt.Rows.Count);
            var row = dt.Rows[0];
            var user = users[0];
            Assert.AreEqual(user.FirstName, row[0]);
            Assert.AreEqual(user.LastName, row[1]);
            Assert.AreEqual(user.Age, row[2]);
        }

        [Test]
        public void AsDataTable_GivenOneItemListAndNullableValueNotSet_ShouldReturnDataTable()
        {
            // Arrange
            var users = new List<User> 
            { 
                new User { FirstName = "Bob", LastName="Smith"},
            };

            // Act
            var dt = users.AsDataTable<User>();
            // Assert
            Assert.AreEqual(1, dt.Rows.Count);
            var row = dt.Rows[0];
            var user = users[0];
            Assert.AreEqual(user.FirstName, row[0]);
            Assert.AreEqual(user.LastName, row[1]);
            Assert.IsFalse(user.Age.HasValue);
            Assert.AreEqual(DBNull.Value, row[2]);
        }

        [Test]
        public void AsDataTable_GivenManyItemList_ShouldReturnDataTable()
        {
            // Arrange
            var users = new List<User> 
            { 
                new User { FirstName = "Bob", LastName="Smith"},
                new User { FirstName = "Cindy", LastName="Jones"},
                new User { FirstName = "Amy", LastName="Black"},
            };

            // Act
            var dt = users.AsDataTable<User>();
            // Assert
            Assert.AreEqual(3, dt.Rows.Count);
        }


        public class User
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int? Age { get; set; }
        }
    }
}
