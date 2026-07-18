import pytest
from lab13 import run_sort

def test_quicksort():
    assert run_sort([47,12,83,5,61,29,74,38,90,16,55,42,7,68,31,99,23,50,14,77]) == [5,7,12,14,16,23,29,31,38,42,47,50,55,61,68,74,77,83,90,99]
    assert run_sort([1,2,3,4,5]) == [1,2,3,4,5]
    assert run_sort([5,4,3,2,1]) == [1,2,3,4,5]
    assert run_sort([7,7,7,7,7]) == [7,7,7,7,7]
    assert run_sort([42]) == [42]
    assert run_sort([9,1]) == [1,9]
    assert run_sort([-3,-1,-7,-2,-5]) == [-7,-5,-3,-2,-1]
    assert run_sort([3,-2,7,-5,0]) == [-5,-2,0,3,7]


if __name__ == "__main__":
    pytest.main([__file__, "-v"])