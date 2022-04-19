using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Start.Server.Models;

namespace Start_Tests {
	[TestClass]
	public class UnitTest1 : UnitTestWithDb {
		public TestContext TestContext { get; set; }

		[TestMethod]
		public override void TestDatabaseOK() {
			base.TestDatabaseOK();
		}

		[TestMethod]
		public void TestMethod1() {
			TestContext.WriteLine("Running TestMethod1 from TestContext");
		}
	}
}
