using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chua_MExer04
{
    internal class Test
    {
        [TestCase("cos(3.14159 + 1) + 3 asin(3)", new[] { "cos" })]
        [TestCase("8", new[] { "8" })]
        [TestCase("8+9", new[] { "8", "9", "+" })]
        [TestCase("(8+9)", new[] { "8", "9", "+" })]
        [TestCase("(6 + 2) * 5 - 8 / 4", new[] { "6", "2", "+", "5", "*", "8", "4", "/", "-" })]
        [TestCase("[(9 * 8) - 6 + 4.189 / 9 ^ (1 / 2)]", new[] { "9", "8", "*", "6", "-", "4.189", "9", "1", "2", "/", "^", "/", "+" })]
        public void ToPostFix_GivenInfix_ComparePostfixResult(string expression, string[] expectedPostfixArray)
        {
            //arrange
            //act
            var postfix = InfixToPostfixConverter.ToPostFix(expression);

            //assert
            var expectedPostFix = new Queue<string>();
            foreach (string s in expectedPostfixArray)
            {
                 expectedPostFix.Enqueue(s);
            }
            Assert.That(postfix, Is.EqualTo(expectedPostFix));

        }


        
        [TestCase(new[] { "3.14159", "1", "+", "cos", "5", "+" }, 17.1f)]

        public void Evaluate_GivenExpression_ReturnExpected(string[] postfix, float expectedResult)
        {
            var postfixQueue = new Queue<string>();
            foreach (string s in postfix) postfixQueue.Enqueue(s);

            float result = PostfixEvaluator.Evaluate(postfixQueue);

            Assert.That(result, Is.EqualTo(expectedResult));

        }
    }
}
