using kata.ms.shoppingbasket.domain;
using kata.ms.shoppingbasket.tests.Extensions;
using kata.ms.shoppingbasket.web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace kata.ms.shoppingbasket.Tests
{
    public class UnitTestBase
    {

        #region : Attribute declaration tests.

        protected bool HasMethodBeenDeclared<T>(string method)
        {
            MethodInfo methodInfo = typeof(T).GetMethod(method);
            return methodInfo != null;
        }

        protected bool MethodCallsMethod(MethodInfo parent, string method)
        {
            bool ret = false;

            var body = parent.GetMethodBody();

            return ret;
        }

        // Note: MemberInfo is inheritance root for Type, MethodInfo, and FieldInfo 
        protected T GetAttribute<T>(MemberInfo member)
        {
            return (T)member.GetCustomAttributes(typeof(T), true)[0];
        }
        protected MethodInfo MethodOf(Expression<System.Action> expression)
        {
            MethodCallExpression body = (MethodCallExpression)expression.Body;
            return body.Method;
        }

        protected bool MethodDeclaresAttribute<T>(Expression<System.Action> expression)
        {
            MemberInfo member = MethodOf(expression);
            return MemberDeclaresAttribute<T>(member);
        }

        protected bool MemberDeclaresAttribute<T>(MemberInfo member)
        {
            bool inherited = false;
            return member.GetCustomAttributes(typeof(T), inherited).Any();
        }

        #endregion : Attribute declaration tests.


    }
    public class ShoppingBasketControllerShould : UnitTestBase
    {
        private Mock<IDataSourceService<ShoppingBasket>> _dataServiceMock;
        private ShoppingBasketController _sut;

        // used to create:
        // ShoppingBasket (empty)
        // IDataSourceService (empty)
        // ShoppingBasketController (empty) + Constructor
        [SetUp]
        public void Setup()
        {
            _dataServiceMock = new Mock<IDataSourceService<ShoppingBasket>>();
            _sut = new ShoppingBasketController(_dataServiceMock.Object);
        }

        [Test]
        public void Declare_a_route_attribute()
        {
            // design: ensure class declares the route attribute
            MemberInfo member = _sut.GetType();
            Assert.IsTrue(MemberDeclaresAttribute<RouteAttribute>(member));

            RouteAttribute attr = GetAttribute<RouteAttribute>(member);
            Assert.AreEqual("api/[controller]",attr.Value());
        }

        [Test]
        public void Declare_a_produces_json_attribute()
        {
            // design : ensures class declares the produces attribute.
            // A filter that specifies the expected System.Type the action will return and the supported response content types. The ContentTypes value is used to set ContentTypes.
            // in this case is also important we ensure the ContentType defined is json.

            // act
            MemberInfo member = _sut.GetType();

            // assert
            Assert.IsTrue(MemberDeclaresAttribute<ProducesAttribute>(member));
            ProducesAttribute attr = GetAttribute<ProducesAttribute>(member);
            Assert.AreEqual(attr.Value(), actual: "application/json");
        }

        [Test]
        public void Declare_ApiController_Attribute()
        {
            // design : ensures class marked as an api controller

            // act
            MemberInfo member = _sut.GetType();

            // assert
            Assert.IsTrue(MemberDeclaresAttribute<ApiControllerAttribute>(member));
        }

        // used to create:
        // IDataSourceService
        // IDataSourceService.Get()
        // ShoppingBasketController.Get() 
        [Test]
        public async void Get_return_Ok_when_item_found()
        {
            // arrange
            _dataServiceMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(null);
            // act 
            var result = await _sut.Get();

            // assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            
        }
    }
}