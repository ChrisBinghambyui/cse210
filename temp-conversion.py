def get_unit(t1):
    if t1 == "c" or t1 =="C" or t1 =="celsius" or t1 == "Celsius":
        return 1
    elif t1 == "f" or t1 == "F" or t1 == "fahrenheit" or t1 == "Fahrenheit":
        return 2
    else:
        return 3

def get_converted(unit, number):
    if unit == 1:
        return f2c(number)
    elif unit == 2:
        return c2f(number)
    else:
        return number


def f2c(number):
    return (number - 32) * (5/9)

def f2k(number):
    return (number - 32) * (5/9) + 273.15

def c2f(number):
    return number * (9/5) + 32

def c2k(number):
    return number + 273.15

def k2f(number):
    return (number - 273.15) * (9/5) + 32

def k2c(number):
    return number - 273.15
