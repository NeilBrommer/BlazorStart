﻿using System;
using IdentityServer4.EntityFramework.Options;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Start.Server.Data;
using Start.Server.Models;

namespace Start_Tests {
	public class UnitTestWithDb : IDisposable {
		private const string InMemoryConnectionString = "DataSource=:memory:";
		private SqliteConnection _connection;

		protected readonly ApplicationDbContext _db;

		public UnitTestWithDb() {
			_connection = new SqliteConnection(InMemoryConnectionString);
			_connection.Open();

			var options = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseSqlite(_connection)
				.Options;

			this._db = new ApplicationDbContext(options,
				Options.Create(new OperationalStoreOptions()));
			this._db.Database.EnsureCreated();
		}

		protected void ResetDb() {
			_db.Database.EnsureDeleted();
			_db.Database.EnsureCreated();
		}

		protected void FillDbTestData() {
			ApplicationUser testUser = new ApplicationUser {
				Id = "test_user",
				UserName = "test_user"
			};
			_db.Users.Add(testUser);
			_db.SaveChanges();

			BookmarkContainer testContainer = new BookmarkContainer(testUser.Id, "Test Container",
				0);
			_db.BookmarkContainers.Add(testContainer);
			_db.SaveChanges();

			BookmarkGroup testGroup = new BookmarkGroup("Test Group", "#000000", 0,
				testContainer.BookmarkContainerId);
			_db.BookmarkGroups.Add(testGroup);
			_db.SaveChanges();

			Bookmark testBookmark = new Bookmark("Test Bookmark", "http://example.com",
				"Test Notes", 0, testGroup.BookmarkGroupId);
			_db.Bookmarks.Add(testBookmark);
			_db.SaveChanges();
		}

		/// <summary>
		/// Checks the DB connection works. Note that MSTest won't run this - you need to do so in
		/// inheriting classes like this:
		///
		/// <code>
		/// [TestMethod]
		/// public override void TestDatabaseOK() {
		/// 	base.TestDatabaseOK();
		/// }
		/// </code>
		/// </summary>
		[TestMethod]
		public virtual void TestDatabaseOK() {
			Assert.IsTrue(this._db.Database.CanConnect());
		}

		public void Dispose() {
			_connection.Close();
		}
	}
}
