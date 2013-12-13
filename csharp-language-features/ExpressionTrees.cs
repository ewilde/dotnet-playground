// -----------------------------------------------------------------------
// <copyright file="ExpressionTrees.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace csharp_language_features
{
    using System;
    using System.Linq.Expressions;

    public class ExpressionTrees
    {
        public void AddTwoNumbersExpression(int a, int b)
        {
            Expression<Func<int, int, int>> adderExpression = (item1, item2) => item1 + item2;

            Console.WriteLine("NodeType: {0}.", adderExpression.NodeType);
            Console.WriteLine("Body: {0}, type: {1}.", adderExpression.Body, adderExpression.Body.GetType());
            Console.WriteLine("Return type: {0}.", adderExpression.ReturnType);

            BinaryExpression binaryExpression = (BinaryExpression)adderExpression.Body;

        }
    }
}