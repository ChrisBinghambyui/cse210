def quicksort(A, left, right):
    if left >= right:
        return

    pivot = A[(left + right) // 2]
    i = left
    j = right

    while i <= j:
        while A[i] < pivot:
            i += 1
        while A[j] > pivot:
            j -= 1
        if i <= j:
            A[i], A[j] = A[j], A[i]
            i += 1
            j -= 1

    quicksort(A, left, j)
    quicksort(A, i, right)


def run_sort(array):
    quicksort(array, 0, len(array) - 1)
    return array