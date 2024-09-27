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
        [TestCase("(3+4)(5-4)", new[] { "3", "4", "+", "5", "4", "-", "*" })]
        [TestCase("(3+4)*(5-4)", new[] { "3", "4", "+", "5", "4", "-", "*" })]
        [TestCase("5^[cos(sin(3+1)-5)]", new[] { "5", "3", "1", "+", "sin", "5", "-", "cos", "^" })]
        [TestCase("(cos(3/2*5^6))+(5^(cos(2/3)))", new[] { "3", "2", "/", "5", "6", "^", "*", "cos", "5", "2", "3", "/", "cos", "^", "+" })]
        [TestCase("cos(3.14159 + 1) +  asin(1)", new[] { "3.14159", "1", "+", "cos", "1", "asin", "+" })]
        [TestCase("sin(3) + 5", new[] { "3", "sin", "5", "+" })]
        //[TestCase("8", new[] { "8" })]
        //[TestCase("8+9", new[] { "8", "9", "+" })]
        //[TestCase("(8+9)", new[] { "8", "9", "+" })]
        //[TestCase("(6 + 2) * 5 - 8 / 4", new[] { "6", "2", "+", "5", "*", "8", "4", "/", "-" })]
        //[TestCase("[(9 * 8) - 6 + 4.189 / 9 ^ (1 / 2)]", new[] { "9", "8", "*", "6", "-", "4.189", "9", "1", "2", "/", "^", "/", "+" })]
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


        [TestCase(new[]{ "3", "4", "+", "5", "4", "-", "*" }, 7f)]
        [TestCase(new[]{ "5", "3", "1", "+", "sin", "5", "-", "cos", "^" }, 4.02115154f)]
        [TestCase(new[]{ "3", "2", "/", "5", "6", "^", "*", "cos", "5", "2", "3", "/", "cos", "^", "+" }, 3.88728285f)]
        [TestCase(new[]{ "3.14159", "1", "+", "cos", "1", "asin", "+" }, 1.03049195f)] 
        [TestCase(new[] { "3", "sin", "5", "+" }, 5.14111996f)]
        
        public void Evaluate_GivenExpression_ReturnExpected(string[] postfix, float expectedResult)
        {
            var postfixQueue = new Queue<string>();
            foreach (string s in postfix) postfixQueue.Enqueue(s);

            float result = PostfixEvaluator.Evaluate(postfixQueue);

            Assert.That(result, Is.EqualTo(expectedResult));

        }
    }
}
