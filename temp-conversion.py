def main():
    temp = input("Choose a temperature unit type: ")
    temp = get_unit(temp)
    assert temp < 4

    num = float(input("Enter the temperature number: "))
    temps = []
    temps = perform_conversions(temp, num)
    print(f"Kelvin: {temps(1)}")
    print(f"Fahrenheit: {temps(2)}")
    print(f"Celsius: {temps(3)}")


def get_unit(t1):
    if t1 == "c" or t1 =="C" or t1 =="celsius" or t1 == "Celsius":
        return 1
    elif t1 == "f" or t1 == "F" or t1 == "fahrenheit" or t1 == "Fahrenheit":
        return 2
    elif t1 == "k" or t1 == "K" or t1 == "kelvin" or t1 == "Kelvin":
        return 3
    else:
        return 4



def get_converted(unit, number):
    if unit == 1:
        return f2c(number)
    elif unit == 2:
        return c2f(number)
    else:
        return number


def perform_conversions (unit, number):
    if unit == 1:
        k = c2k(number)
        f = c2f(number)
        c = number
    elif unit == 2:
        k = f2k(number)
        f = number
        c = f2c(number)
    elif unit == 3:
        k = number
        f = k2f(number)
        c = k2c(number)
    return [k, f, c]


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
