# 1. Name:
#      Chris Bingham
# 2. Assignment Name:
#      Lab 08 : Sublist Sort
# 3. Assignment Description:
#      implement a natural merge sort using sublist/run detection
# 4. What was the hardest part? Be as specific as possible.
#      i had trouble keeping a and b straight in my head, i was trying to juggle too much mentally and just kept making myself more adn more frustruated lol
# 5. How long did it take for you to complete this assignment?
#      4 hrs 30 min

import pytest
import random

def advance(source, x):
    val = source[x]
    x += 1
    return x, val


def find_sublist(source, x):
    start = x
    while x < len(source) - 1 and source[x] <= source[x + 1]:
        x += 1
    end = x
    x += 1
    return x, start, end


def copy_leftover(source, a, end_a, source_b, b, end_b, dest, y):
    while a <= end_a:
        dest[y] = source[a]
        a += 1
        y += 1
    while b <= end_b:
        dest[y] = source_b[b]
        b += 1
        y += 1
    return y


def merge_sublists(source, start_a, end_a, start_b, end_b, dest, y):
    a = start_a
    b = start_b
    while a <= end_a and b <= end_b:
        if source[a] <= source[b]:
            x, val = advance(source, a)
            a = x
        else:
            x, val = advance(source, b)
            b = x
        dest[y] = val
        y += 1
    y = copy_leftover(source, a, end_a, source, b, end_b, dest, y)
    return y


def copy_remaining(source, start, end, dest, y):
    x = start
    while x <= end:
        dest[y] = source[x]
        x += 1
        y += 1
    return y


def sublist_sort(source):
    if len(source) <= 1:
        return source

    source = list(source)
    n = len(source)

    while True:
        dest = [0] * n
        x = 0
        y = 0
        sublist_count = 0

        while x < n:
            x, start_a, end_a = find_sublist(source, x)
            sublist_count += 1

            if x >= n:
                y = copy_remaining(source, start_a, end_a, dest, y)
            else:
                x, start_b, end_b = find_sublist(source, x)
                sublist_count += 1
                y = merge_sublists(source, start_a, end_a, start_b, end_b, dest, y)

        source = dest

        if sublist_count <= 2:
            break

    return source



def test_sublist_sort():
    assert sublist_sort([3, 4, 7, 8, 12]) == [3, 4, 7, 8, 12]
    assert sublist_sort([5, 4, 3, 2, 1]) == [1, 2, 3, 4, 5]
    assert sublist_sort([9]) == [9]
    assert sublist_sort([]) == []
    assert sublist_sort([5, 3, 5, 1, 5, 2]) == [1, 2, 3, 5, 5, 5]
    assert sublist_sort([47,12,35,8,91,23,56,4,78,15,63,29,7,44,82,19,55,31,66,3]) == \
        [3,4,7,8,12,15,19,23,29,31,35,44,47,55,56,63,66,78,82,91]
    assert sublist_sort([7, 7, 7, 7, 7]) == [7, 7, 7, 7, 7]
    assert sublist_sort(['d', 'b', 'e', 'a', 'c']) == ['a', 'b', 'c', 'd', 'e']


if __name__ == "__main__":
    unsorted_list = [random.randint(1, 100) for _ in range(20)]
    print(f"Original list: {unsorted_list}")
    
    sorted_list = sublist_sort(unsorted_list)
    print(f"Sorted list:   {sorted_list}")
    
    # print("\nunit tests:")
    # result = pytest.main([__file__])