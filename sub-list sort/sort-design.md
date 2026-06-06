random numbers: 47, 12, 35, 8, 91, 23, 56, 4, 78, 15, 63, 29, 7, 44, 82, 19, 55, 31, 66, 3

sublists
    [47] [12, 35] [8, 91] [23, 56] [4, 78] [15, 63] [29] [7, 44, 82] [19, 55] [31, 66] [3]
merging
    12, 35, 47, 8, 23, 56, 91, 4, 15, 63, 78, 7, 29, 44, 82, 19, 31, 55, 66, 3

pass 2
    [12, 35, 47] [8, 23, 56, 91] [4, 15, 63, 78] [7, 29, 44, 82] [19, 31, 55, 66] [3]
merging
    8, 12, 23, 35, 47, 56, 91, 4, 7, 15, 29, 44, 63, 78, 82, 3, 19, 31, 55, 66

pass 3
    [8, 12, 23, 35, 47, 56, 91] [4, 7, 15, 29, 44, 63, 78, 82] [3, 19, 31, 55, 66]
    4, 7, 8, 12, 15, 23, 29, 35, 44, 47, 56, 63, 78, 82, 91, 3, 19, 31, 55, 66

pass 4
    [4, 7, 8, 12, 15, 23, 29, 35, 44, 47, 56, 63, 78, 82, 91] [3, 19, 31, 55, 66]
    3, 4, 7, 8, 12, 15, 19, 23, 29, 31, 35, 44, 47, 55, 56, 63, 66, 78, 82, 91


Approach
The sub list defines groups by going through each member of the list until it finds a number that is lower than the previous, which ends the previous group and starts a new one. It then looks at the collection of groups, then pairs them up, mashing them together until they're a single group thats perfectly ordered from smallest to largest. It then starts over, disbanding the old groups and making new ones the same way (usually getting the same number of groups as at the end of the last pass but not always). this process repeats until the entire list of numbers is perfectly sorted.


pseudocode

SUBLIST_SORT(source):
  n = length of source
  destination = array of size n

  REPEAT:
    x = 0
    y = 0
    sublistCount = 0

    WHILE x < n:
      startA = x
      WHILE x < n-1 AND source[x] <= source[x+1]:
        x += 1
      endA = x
      x += 1
      sublistCount += 1

      IF x >= n:
        copy source[startA to endA] into destination starting at y
        y += (endA - startA + 1)
      ELSE:
        startB = x
        WHILE x < n-1 AND source[x] <= source[x+1]:
          x += 1
        endB = x
        x += 1
        sublistCount += 1

        a = startA
        b = startB
        WHILE a <= endA AND b <= endB:
          IF source[a] <= source[b]:
            destination[y] = source[a]
            a += 1
          ELSE:
            destination[y] = source[b]
            b += 1
          y += 1

        copy any leftover from A into destination starting at y
        copy any leftover from B into destination starting at y

    source = destination

  UNTIL sublistCount <= 2

  RETURN source



copilot pseudocode
FUNCTION NaturalMergeSort(list):

    WHILE True:

        // Step 1: Split list into increasing runs (groups)
        groups = []
        currentGroup = [list[0]]

        FOR i FROM 1 TO length(list) - 1:
            IF list[i] >= list[i - 1]:
                APPEND list[i] TO currentGroup
            ELSE:
                APPEND currentGroup TO groups
                currentGroup = [list[i]]

        APPEND currentGroup TO groups

        // If only one group, list is fully sorted
        IF length(groups) == 1:
            RETURN groups[0]

        // Step 2: Merge pairs of groups
        newList = []
        i = 0

        WHILE i < length(groups):
            IF i + 1 < length(groups):
                merged = Merge(groups[i], groups[i + 1])
                APPEND merged TO newList
                i = i + 2
            ELSE:
                // Odd group left, carry it forward
                APPEND groups[i] TO newList
                i = i + 1

        // Step 3: Flatten merged groups back into list
        list = Concatenate(newList)


FUNCTION Merge(A, B):
    result = []
    i = 0
    j = 0

    WHILE i < length(A) AND j < length(B):
        IF A[i] <= BAPPEND A[i] TO result
            i = i + 1
        ELSE:
            APPEND B[j] TO result
            j = j + 1

    // Append remaining elements
    WHILE i < length(A):
        APPEND A[i] TO result
        i = i + 1

    WHILE j < length(B):
        APPEND B[j] TO result
        j = j + 1

    RETURN result



compare and contrast
i feel like the copilot version would be less efficient as it looks like it uses more groups and just uses more variables overall