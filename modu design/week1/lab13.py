# 1. Name:
#      Chris Bingham
# 2. Assignment Name:
#      Lab 13 : Segregation Sort Program
# 3. Assignment Description:
#      sort an array of numbers using a segregation sort
# 4. What was the hardest part? Be as specific as possible.
#      This week wasn't hard at all actually, the pseudocode from last week kinda carried
# 5. How long did it take for you to complete the assignment?
#      like an hour, tops



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