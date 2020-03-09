using NUnit.Framework;
using System;

namespace Qualified.kata.markdownparser.tests
{

    [TestFixture]
    public class MarkdownParserTest
    {
        [Test]
        public void BasicValidCases()
        {
            
            Assert.AreEqual("<h1># x</h1>", Challenge.MarkdownParser("# # x"));
            Assert.AreEqual("<h1>x</h1>", Challenge.MarkdownParser("# x"));
            Assert.AreEqual("<h1>header</h1>", Challenge.MarkdownParser("# header"));
            Assert.AreEqual("<h2>smaller header</h2>", Challenge.MarkdownParser("## smaller header"));
        }
        [Test]
        public void BasicInvalidCases()
        {
            Assert.AreEqual("#Invalid", Challenge.MarkdownParser("#Invalid"));
            Assert.AreEqual("####### x", Challenge.MarkdownParser("####### x"));
        }
    }
}