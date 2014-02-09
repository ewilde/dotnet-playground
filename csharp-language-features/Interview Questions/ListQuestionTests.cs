using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace csharp_language_features.InterviewQuestions
{
    [TestFixture]
    public class ListQuestionTests
    {
        [Test]
        public void Reverse_empty_list_returns_empty_list()
        {
            var items = new List<int>();
            
            ListQuestions.Reverse(items);

            Assert.That(items, Is.Empty);
        }

        [Test]
        public void Reverse_list_with_one_item_returns_list_with_one_item()
        {
            var items = new List<int> { 1 };
            
            ListQuestions.Reverse(items);

            Assert.That(items.Count, Is.EqualTo(1));
        }

        [Test]
        public void Reverse_list_with_even_number_of_items_returns_reversed_list()
        {
            var items = new List<int> { 1, 2, 3, 4, 5, 6 };
            
            ListQuestions.Reverse(items);

            Assert.That(items, Is.EquivalentTo(new[] {6, 5, 4, 3, 2, 1}));
        }

        [Test]
        public void Reverse_list_with_odd_number_of_items_returns_reversed_list()
        {
            var items = new List<int> { 1, 2, 3, 4, 5};
            
            ListQuestions.Reverse(items);

            Assert.That(items, Is.EquivalentTo(new[] {5, 4, 3, 2, 1}));
        }

        [Test]
        public void Merge_with_empty_lists_returns_empty_list()
        {
            var result = ListQuestions.Merge(new List<int>(), new List<int>());

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void Merge_with_empty_right_lists_returns_left_list()
        {
            var result = ListQuestions.Merge(new List<int> { 1, 2, 3}, new List<int>());

            Assert.That(result, Is.EquivalentTo(new[] {1, 2, 3}));
        }

        [Test]
        public void Merge_with_empty_left_lists_returns_right_list()
        {
            var result = ListQuestions.Merge(new List<int>(), new List<int> { 1, 2, 3});

            Assert.That(result, Is.EquivalentTo(new[] {1, 2, 3}));
        }

        [Test]
        public void Merge_with_even_lists_with_same_items_returns_duplicate_sorted_list()
        {
            var result = ListQuestions.Merge(new List<int> {1, 2, 3}, new List<int> { 1, 2, 3});

            Assert.That(result, Is.EquivalentTo(new[] {1, 1, 2, 2, 3, 3}));
        }

        [Test]
        public void Merge_with_smaller_higher_left_list_returns_duplicate_sorted_list()
        {
            var result = ListQuestions.Merge(new List<int> { 9, 10}, new List<int> { 1, 2, 3});

            Assert.That(result, Is.EquivalentTo(new[] {1, 2, 3, 9, 10}));
        }

        [Test]
        public void Merge_with_smaller_higher_right_list_returns_duplicate_sorted_list()
        {
            var result = ListQuestions.Merge(new List<int> { 9, 10}, new List<int> { 11});

            Assert.That(result, Is.EquivalentTo(new[] {9, 10, 11}));
        }
    }
}
