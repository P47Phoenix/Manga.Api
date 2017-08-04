using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ifx.JsonApi.Test
{
    [TestClass]
    public class JsonApiIdTest
    {
        [TestMethod]
        public void GetIdTest()
        {
            var model = new MockModel
            {
                StringValue = "StringValue",
                MockId = 3424,
                IntValue = 52
            };

            var jsonApiId = new JsonApiId<MockModel>(model);
            
            Assert.AreEqual(3424, jsonApiId.Get());
        }

        [TestMethod]
        public void SetIdTest()
        {
            var model = new MockModel
            {
                StringValue = "StringValue",
                MockId = 3424,
                IntValue = 52
            };

            var jsonApiId = new JsonApiId<MockModel>(model);

            Assert.AreEqual(3424, jsonApiId.Get());
            Assert.AreEqual(3424, model.MockId);

            jsonApiId.Set(23);

            Assert.AreEqual(23, jsonApiId.Get());
            Assert.AreEqual(23, model.MockId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetIdTestWithWrongValueType()
        {
            var model = new MockModel
            {
                StringValue = "StringValue",
                MockId = 3424,
                IntValue = 52
            };

            var jsonApiId = new JsonApiId<MockModel>(model);
            
            jsonApiId.Set("sesdfsd");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetIdTestModelWithNoId()
        {
            var model = new MockNoIdModel()
            {
                Id = 3
            };

            var jsonApiId = new JsonApiId<MockNoIdModel>(model);
            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetIdTestModelWithToManyIds()
        {
            var model = new MockToManyIdModel()
            {
                Id = 3,
                IdTwo = 4
            };

            var jsonApiId = new JsonApiId<MockToManyIdModel>(model);
        }
    }
}
