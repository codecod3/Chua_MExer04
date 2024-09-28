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
        //InfixtoPostfix

        [TestCase("(3+4)(5-4)", new[] { "3", "4", "+", "5", "4", "-", "*" })]
        [TestCase("(3+4)*(5-4)", new[] { "3", "4", "+", "5", "4", "-", "*" })]
        [TestCase("(5)(cos(1))", new[] { "5", "1", "cos", "*" })]
        [TestCase("(cos(32-1))^[(cos(3/2))(sin(5))]", new[] { "32","1","-","cos","3","2","/","cos","5","sin", "*","^"})]
        [TestCase("[cos(3.14159/2)][sin(0)]", new[] { "3.14159","2", "/", "cos", "0", "sin", "*" })]
        public void ToPostfix_ClosingAndOpeningBracketsParenthesisCurlyBracesShouldBeImpliedMultiplication_ShouldPushMultiplication(string expression, string[] expectedPostfixArray)
        {


            var postfix = InfixToPostfixConverter.ToPostFix(expression);
            var expectedPostFix = new Queue<string>();
            foreach (string s in expectedPostfixArray)
            {
                expectedPostFix.Enqueue(s);
            }
            Assert.That(postfix, Is.EqualTo(expectedPostFix));

        }

        [TestCase("3.1416*8-51", new[] { "3.1416", "8", "*", "51", "-" })]
        [TestCase("3.14159 + cos(3)", new[] { "3.14159", "3", "cos", "+" })]
        [TestCase("(5.5)(5.5)", new[] { "5.5","5.5","*" })]
        [TestCase("(tan(3.5))(sin(2.5))", new[] { "3.5","tan","2.5","sin","*" })]
        public void ToPostfix_NumberIsInDecimal_ShouldAcceptDecimal(string expression, string[] expectedPostfixArray)
        {
            var postfix = InfixToPostfixConverter.ToPostFix(expression);
            var expectedPostFix = new Queue<string>();
            foreach (string s in expectedPostfixArray)
            {
                expectedPostFix.Enqueue(s);
            }
            Assert.That(postfix, Is.EqualTo(expectedPostFix));

        }

        [TestCase("sin(3) + 5", new[] { "3","sin","5", "+" })]
        [TestCase("cos(3) + 5", new[] { "3","cos","5", "+" })]
        [TestCase("tan(3) + 5", new[] { "3","tan","5", "+" })]
        [TestCase("sin(3) + cos(3)", new[] { "3","sin","3","cos","+" })]
        [TestCase("[sin(3)][cos(3)]", new[] { "3","sin","3","cos","*" })]
        [TestCase("(sin(3))^(sin(5/2*5))", new[] { "3", "sin", "5", "2", "/", "5", "*", "sin", "^" })]
        public void ToPostfix_TrigonometricFunctions_ShouldConverTrigFunctionsToPostfix(string expression, string[] expectedPostfixArray)
        {
            var postfix = InfixToPostfixConverter.ToPostFix(expression);
            var expectedPostFix = new Queue<string>();
            foreach (string s in expectedPostfixArray)
            {
                expectedPostFix.Enqueue(s);
            }
            Assert.That(postfix, Is.EqualTo(expectedPostFix));

        }


        [TestCase("asin(3) + 5", new[] { "3", "asin", "5", "+" })]
        [TestCase("acos(3) + 5", new[] { "3", "acos", "5", "+" })]
        [TestCase("atan(3) + 5", new[] { "3", "atan", "5", "+" })]
        [TestCase("asin(1) + acos(1)", new[] { "1", "asin", "1", "acos", "+" })]
        [TestCase("[asin(1)][acos(1)]", new[] { "1", "asin", "1", "acos", "*" })]
        [TestCase("(asin(1))^(sin(5/2*5))", new[] { "1", "asin", "5", "2", "/", "5", "*", "sin", "^" })]
        public void ToPostfix_InverseTrigonometricFunctions_ShouldConverInverseTrigFunctionsToPostfix(string expression, string[] expectedPostfixArray)
        {
            var postfix = InfixToPostfixConverter.ToPostFix(expression);
            var expectedPostFix = new Queue<string>();
            foreach (string s in expectedPostfixArray)
            {
                expectedPostFix.Enqueue(s);
            }
            Assert.That(postfix, Is.EqualTo(expectedPostFix));

        }


        //Postfix Evaluator

        [TestCase(new[] { "3", "4", "+", "5", "4", "-", "*" }, 7f)]
        [TestCase(new[] { "5", "1", "cos", "*" }, 2.70151138f)]
        [TestCase(new[] { "32", "1", "-", "cos", "3", "2", "/", "cos", "5", "sin", "*", "^" }, 1.00606298f)]
        [TestCase(new[] { "3.14159", "2", "/", "cos", "0", "sin", "*" }, 0f)]

        public void Evaluate_ClosingAndOpeningBracketsParenthesisCurlyBracesShouldBeImpliedMultiplication_ShouldMultiply(string[] postfix, float expectedResult)
        {
            var postfixQueue = new Queue<string>();
            foreach (string s in postfix) postfixQueue.Enqueue(s);

            float result = PostfixEvaluator.Evaluate(postfixQueue);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(new[] { "3.1416", "8", "*", "51", "-" }, -25.8672009f)]
        [TestCase(new[] { "3.14159", "3", "cos", "+" }, 2.1515975f)]
        [TestCase(new[] { "5.5", "5.5", "*" }, 30.25f)]
        [TestCase(new[] { "3.5", "tan", "2.5", "sin", "*" }, 0.224179059f)]

        public void Evaluate_NumberIsInDecimal_ShouldAcceptAndEvaluateDecimal(string[] postfix, float expectedResult)
        {
            var postfixQueue = new Queue<string>();
            foreach (string s in postfix) postfixQueue.Enqueue(s);

            float result = PostfixEvaluator.Evaluate(postfixQueue);

            Assert.That(result, Is.EqualTo(expectedResult));

        }

        [TestCase(new[] { "3", "sin", "5", "+" }, 5.14111996f)]
        [TestCase(new[] { "3", "cos", "5", "+" }, 4.01000738f)]
        [TestCase(new[] { "3", "tan", "5", "+" }, 4.85745335f)]
        [TestCase(new[] { "3", "sin", "3", "cos", "+" }, -0.848872483f)]
        [TestCase(new[] { "3", "sin", "3", "cos", "*" }, -0.139707744f)]
        [TestCase(new[] { "3", "sin", "5", "2", "/", "5", "*", "sin", "^" }, 1.13867795f)]

        public void Evaluate__TrigonometricFunctions_ShouldEvaluateTrigFunctions(string[] postfix, float expectedResult)
        {
            var postfixQueue = new Queue<string>();
            foreach (string s in postfix) postfixQueue.Enqueue(s);

            float result = PostfixEvaluator.Evaluate(postfixQueue);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(new[] { "1", "asin", "5", "+" }, 6.57079649f)]
        [TestCase(new[] { "1", "acos", "5", "+" }, 5f)]
        [TestCase(new[] { "3", "atan", "5", "+" }, 6.24904585f)]
        [TestCase(new[] { "1", "asin", "1", "acos", "+" }, 1.57079637f)]
        [TestCase(new[] { "1", "asin", "1", "acos", "*" }, 0f)]
        [TestCase(new[] { "1", "asin", "5", "2", "/", "5", "*", "sin", "^" }, 0.970494211f)]
        
        public void Evaluate_InverseTrigonometricFunctions_ShouldEvaluateInverseTrigFunctions(string[] postfix, float expectedResult)
        {
            var postfixQueue = new Queue<string>();
            foreach (string s in postfix) postfixQueue.Enqueue(s);

            float result = PostfixEvaluator.Evaluate(postfixQueue);

            Assert.That(result, Is.EqualTo(expectedResult));
        }








        //Extra

        [TestCase("(3+4)(5-4)", new[] { "3", "4", "+", "5", "4", "-", "*" })]
        [TestCase("(3+4)*(5-4)", new[] { "3", "4", "+", "5", "4", "-", "*" })]
        [TestCase("5^[cos(sin(3+1)-5)]", new[] { "5", "3", "1", "+", "sin", "5", "-", "cos", "^" })]
        [TestCase("(cos(3/2*5^6))+(5^(cos(2/3)))", new[] { "3", "2", "/", "5", "6", "^", "*", "cos", "5", "2", "3", "/", "cos", "^", "+" })]
        [TestCase("cos(3.14159 + 1) +  asin(1)", new[] { "3.14159", "1", "+", "cos", "1", "asin", "+" })]
        [TestCase("3.1416*8-51", new[] { "3.1416", "8", "*", "51", "-" })]
        [TestCase("sin(3) + 5", new[] { "3", "sin", "5", "+"})]
        [TestCase("asin(3) + 5 ", new[] { "3", "asin", "5", "+" })]
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
        [TestCase(new[] { "3.14159", "2", "/", "cos", "0", "sin", "*" }, 0f)]
        
        public void Evaluate_GivenExpression_ReturnExpected(string[] postfix, float expectedResult)
        {
            var postfixQueue = new Queue<string>();
            foreach (string s in postfix) postfixQueue.Enqueue(s);

            float result = PostfixEvaluator.Evaluate(postfixQueue);

            Assert.That(result, Is.EqualTo(expectedResult));

        }
    }
}
