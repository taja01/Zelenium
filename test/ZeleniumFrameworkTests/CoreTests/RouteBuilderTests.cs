using System;
using Moq;
using NUnit.Framework;
using ZeleniumFramework.WebDriver.Interfaces;

namespace ZeleniumFrameworkTests.CoreTests
{
    [TestFixture]
    public class RouteBuilderTests
    {
        enum Facebook
        {
            MyPorfile,
            Message,
            FAQ
        }

        enum Gmail
        {
            Inbox,
            Sent,
            FAQ
        }

        class FacebookUrlBuilder : IRouteBuilder<Facebook>
        {
            public string GetUrl(Facebook page)
            {
                return page switch
                {
                    Facebook.Message => "message",
                    Facebook.MyPorfile => "myprofile",
                    _ => throw new NotImplementedException(),
                };
            }
        }

        class GmailUriBuilder : IRouteBuilder<Gmail>
        {
            public string GetUrl(Gmail page)
            {
                return page switch
                {
                    Gmail.Sent => "sent",
                    Gmail.Inbox => "inbox",
                    _ => throw new NotImplementedException(),
                };
            }
        }

        [Test]
        public void FacebookUrlBuilderTest()
        {
            var fbBuilder = new FacebookUrlBuilder();

            Assert.AreEqual("message", fbBuilder.GetUrl(Facebook.Message));
            Assert.AreEqual("myprofile", fbBuilder.GetUrl(Facebook.MyPorfile));

            Assert.Throws<NotImplementedException>(() => fbBuilder.GetUrl(Facebook.FAQ));
        }

        [Test]
        public void GmailUrlBuilderTest()
        {
            var gmBuilder = new GmailUriBuilder();

            Assert.AreEqual("inbox", gmBuilder.GetUrl(Gmail.Inbox));
            Assert.AreEqual("sent", gmBuilder.GetUrl(Gmail.Sent));

            Assert.Throws<NotImplementedException>(() => gmBuilder.GetUrl(Gmail.FAQ));
        }
    }
}
