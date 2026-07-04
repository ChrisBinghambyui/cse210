Step 1 By Hand: 45 minutes
Step 2 Approach: 10 minutes
Step 3 Pseudocode and Structure Chart: 45 minutes
Step 4 Copilot: 5 minutes
Step 5 Compare and Contrast: 20 minutes
Step 6 Update: 20 minutes







sorting:
47, 12, 83, 5, 61, 29, 74, 38, 90, 16, 55, 42, 7, 68, 31, 99, 23, 50, 14, 77

pivot = 16 (index 9)
swap 47/14, 83/7, 61/5, 74/23, meet at 38, out of order so swap pivot in
14, 12, 7, 5, 23, 16, 74, 38, 90, 61, 55, 42, 29, 68, 31, 99, 47, 50, 83, 77

pivot = 7 (index 2, sublist 0-4)
swap 14/5, meet at pivot, stays
5, 12, 7, 14, 23, 16, 74, 38, 90, 61, 55, 42, 29, 68, 31, 99, 47, 50, 83, 77

pivot = 83 (index 12, sublist 6-19)
swap 83/29, meet at 99, out of order so swap pivot in
5, 12, 7, 14, 23, 16, 74, 38, 90, 61, 55, 42, 29, 68, 31, 99, 47, 50, 83, 77

pivot = 5 (sublist 0-1), meets at itself, stays
pivot = 14 (sublist 3-4), meets at itself, stays
5, 12, 7, 14, 23, 16, 74, 38, 90, 61, 55, 42, 29, 68, 31, 99, 47, 50, 83, 77

pivot = 42 (index 11, sublist 6-17)
swap 74/29, 90/31, 61/38, meet at 55, out of order so swap pivot in
77 is single element, done
5, 12, 7, 14, 23, 16, 31, 38, 29, 61, 42, 55, 74, 68, 90, 99, 47, 50, 83, 77

pivot = 38 (sublist 6-9)
swap 38/29, meet at 61, out of order so swap pivot in
pivot = 90 (sublist 11-17)
swap 90/50, meet at 99, out of order so swap pivot in
5, 12, 7, 14, 23, 16, 29, 38, 31, 61, 42, 55, 74, 68, 50, 47, 90, 99, 83, 77

29 is single element, done
pivot = 31 (sublist 8-9), meets at itself, stays
pivot = 50 (sublist 11-15)
swap 55/47, meet at 74, out of order so swap pivot in
99 is single element, done
5, 12, 7, 14, 23, 16, 29, 38, 31, 61, 42, 47, 50, 55, 68, 74, 90, 99, 83, 77

12, 23, 61, 47 are all single elements, done
pivot = 68 (sublist 13-15)
swap 74/55, meet at 68, in order so stays
77 and 83 in order, both done
5, 12, 7, 14, 23, 16, 29, 38, 31, 61, 42, 47, 50, 55, 68, 74, 90, 99, 77, 83

55, 74, 77, 83 all single elements, done

sorted: 5, 7, 12, 14, 16, 23, 29, 31, 38, 42, 47, 50, 55, 61, 68, 74, 77, 83, 90, 99


Approach/explanation:
This method of sorting is different from bubble sort. Here, you select a number (usually whatever number is in the middle of the existing set), and look through the rest of the numbers from the outside in. Once the pointers find numbers that are supposed to be on the other side, they're traded. This will guarantee that by the end the pivot number is now right where they need to be. You then take both sides, and repeat until you're done.

puhsoodocode:
sort(array, left, right):
    if left >= right: return

    p = split(array, left, right)
    sort(array, left, p - 1)
    sort(array, p + 1, right)


split(array, left, right):
    pivot = array[(left + right) / 2]
    L = left
    R = right

    while L <= R:
        while array[L] < pivot: L++
        while array[R] > pivot: R--
        if L <= R:
            swap(array[L], array[R])
            L++
            R--

    if array[R] > pivot: swap(array[R], pivot)
    return R




copilot solution:
procedure quicksort(A, left, right):
    if left >= right:
        return

    pivotIndex = floor((left + right) / 2)
    pivot = A[pivotIndex]

    i = left
    j = right

    while i <= j:
        while A[i] < pivot:
            i = i + 1

        while A[j] > pivot:
            j = j - 1

        if i <= j:
            swap A[i] and A[j]
            i = i + 1
            j = j - 1

    quicksort(A, left, j)
    quicksort(A, i, right)



analysis:
I actually like the copilot solution more, I think it better shows the way this method nudges numbers to the appropriate side of the pivot. Looking back at mine I think It would actually break down if the right list was input. I'm gonna poach the way it handled sweeping the numbers to the right side.



updated code:
sort(array, left, right):
    if left >= right:
        return
    pivot = array[floor((left + right) / 2)]
    L = left
    R = right
    while L <= R:
        while array[L] < pivot:
            L = L + 1
        while array[R] > pivot:
            R = R - 1
        if L <= R:
            swap array[L] and array[R]
            L = L + 1
            R = R - 1
    sort_left(array, left, R)
    sort_right(array, L, right)


sort_left(array, left, R):
    sort(array, left, R)

sort_right(array, L, right):
    sort(array, L, right)