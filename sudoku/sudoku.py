# 1. Name:
#      Chris Bingham
# 2. Assignment Name:
#      Lab 05 : Sudoku Draft
# 3. Assignment Description:
#      Run sudoku, and allow editing of target spaces, then saving and reopening that file.
# 4. What was the hardest part? Be as specific as possible.
#      Just sorta juggling it all mentally, I think I can do more to simplify my functions
# 5. How long did it take for you to complete the assignment?
#      4 hrs (Somehow when handling the get_cell function i got rows and columns swapped, and it took me forever to figure out that that was the problem)

import json
import os


BASE = os.path.dirname(os.path.abspath(__file__))
DEF_FILE = "131.05.Easy.json"


def mk_board():
    return [[0, 0, 0, 0, 0, 0, 0, 0, 0] for _ in range(9)]


def make_path(name):
    if os.path.isabs(name):
        return name
    if os.path.exists(name):
        return os.path.abspath(name)
    return os.path.join(BASE, name)


def load_board(name):
    path = make_path(name)
    with open(path, "r", encoding="utf-8") as f:
        data = json.load(f)

    if isinstance(data, dict):
        data = data.get("board")

    if data is None:
        raise ValueError("No board found.")

    if len(data) != 9:
        raise ValueError("Board needs 9 rows.")

    bd = mk_board()
    for r in range(9):
        if len(data[r]) != 9:
            raise ValueError("Board needs 9 columns.")
        for c in range(9):
            bd[r][c] = data[r][c]

    return bd


def save_board(name, bd):
    path = make_path(name)
    with open(path, "w", encoding="utf-8") as f:
        json.dump({"board": bd}, f, indent=2)


def show_board(bd):
    print("   A B C D E F G H I")
    for r in range(9):
        parts = []
        for c in range(9):
            n = bd[r][c]
            parts.append(" " if n == 0 else str(n))
        line = f"{r + 1}  {' '.join(parts[0:3])}|{' '.join(parts[3:6])}|{' '.join(parts[6:9])}"
        print(line)
        if r in (2, 5):
            print("   -----+-----+-----")


def get_cell(txt):
    if len(txt) != 2:
        return None

    col = txt[0].upper()
    row = txt[1]
    if col < "A" or col > "I" or row < "1" or row > "9":
        return None

    return ord(row) - ord("1"), ord(col) - ord("A")


def main():
    print("Sudoku")

    fn = input(f"Enter board filename to load [{DEF_FILE}]: ").strip()
    if fn == "":
        fn = DEF_FILE

    try:
        bd = load_board(fn)
    except (OSError, ValueError, json.JSONDecodeError) as err:
        print(f"Load failed: {err}")
        bd = mk_board()

    keep = True
    while keep:
        show_board(bd)
        cmd = input("Specify a coordinate to edit or 'Q' to save and quit\n> ").strip().upper()

        if cmd == "Q":
            try:
                save_board(fn, bd)
                print(f"Saved board to {make_path(fn)}")
            except OSError as err:
                print(f"Save failed: {err}")
            keep = False
        else:
            spot = get_cell(cmd)
            if spot is None:
                print("Please use a coordinate like B8 or Q to quit.")
            else:
                r, c = spot
                spot_name = f"{cmd[0].upper()}{cmd[1]}"
                num = input(f"What number goes in {spot_name}? ").strip()
                if num.isdigit() and 0 <= int(num) <= 9:
                    bd[r][c] = int(num)
                else:
                    print("Please enter a number from 0 to 9.")

        print()


if __name__ == "__main__":
    main()
