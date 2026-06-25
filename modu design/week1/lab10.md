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