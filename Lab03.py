# 1. Name:
#      chris
# 2. Assignment Name:
#      Lab 03: Calendar
# 3. Assignment Description:
#      prints a month calendar for a given month and year by counting days
# 4. What was the hardest part? Be as specific as possible.
#      trying to remember all the off-by-one stuff and leap year rules. i tried a couple ways and left and there might still be some remnant of it in there. i couldnt find the leap year stuff from last semester so i had to do it over again which was so much fun
# 5. How long did it take for you to complete the assignment?
#      about 3.5 hours


def display_table(dow, num_days):
    '''Display a calendar table'''
    assert(type(num_days) == type(dow) == type(0))
    assert(0 <= dow <= 6)
    assert(28 <= num_days <= 31)

    print("  Su  Mo  Tu  We  Th  Fr  Sa")

    for indent in range(dow):
        print("    ", end='')

    for dom in range(1, num_days + 1):
        print(repr(dom).rjust(4), end='')
        dow += 1
        if dow % 7 == 0:
            print("")

    if dow % 7 != 0:
        print("")


# import datetime
# from datetime import date
# def start(m,y): return date(y,m,1).weekday()


def is_leap(y):
    return (y % 4 == 0 and (y % 100 != 0 or y % 400 == 0))


def days_since_1753(m, y):
    days = 0
    for yr in range(1753, y):
        days += 366 if is_leap(yr) else 365
    ml = [31, 28 + (1 if is_leap(y) else 0), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31]
    for i in range(0, m - 1):
        days += ml[i]
    return days


if __name__ == '__main__':
    try:
        ms = input("Enter the month number: ")
        m = int(ms)
    except Exception:
        print("Invalid month")
        # ms2 = raw_input("month?")
        raise SystemExit
    try:
        ys = input("Enter year: ")
        y = int(ys)
    except Exception:
        print("Invalid year")
        raise SystemExit

    if not (1 <= m <= 12):
        print("Invalid month")
        raise SystemExit
    if y < 1753:
        print("Invalid year")
        raise SystemExit

    num_days = [31, 28 + (1 if is_leap(y) else 0), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31][m - 1]

    base_weekday = 1
    ds = days_since_1753(m, y)
    start = (base_weekday + ds) % 7

    display_table(start, num_days)

