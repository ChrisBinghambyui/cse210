import pytest

def sort(array, left, right):
    if left >= right:
        return
    pivot = array[(left + right) // 2]
    L = left
    R = right
    while L <= R:
        while array[L] < pivot:
            L += 1
        while array[R] > pivot:
            R -= 1
        if L <= R:
            array[L], array[R] = array[R], array[L]
            L += 1
            R -= 1
    sort(array, left, R)
    sort(array, L, right)

def run_sort(array):
    sort(array, 0, len(array) - 1)
    return array

def test_random_order():
    assert run_sort([47,12,83,5,61,29,74,38,90,16,55,42,7,68,31,99,23,50,14,77]) == [5,7,12,14,16,23,29,31,38,42,47,50,55,61,68,74,77,83,90,99]

def test_already_sorted():
    assert run_sort([1,2,3,4,5]) == [1,2,3,4,5]

def test_reverse_sorted():
    assert run_sort([5,4,3,2,1]) == [1,2,3,4,5]

def test_all_duplicates():
    assert run_sort([7,7,7,7,7]) == [7,7,7,7,7]

def test_single_element():
    assert run_sort([42]) == [42]

def test_two_elements_out_of_order():
    assert run_sort([9,1]) == [1,9]

def test_negative_numbers():
    assert run_sort([-3,-1,-7,-2,-5]) == [-7,-5,-3,-2,-1]

def test_mixed_negative_and_positive():
    assert run_sort([3,-2,7,-5,0]) == [-5,-2,0,3,7]