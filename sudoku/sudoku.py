

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
    return parse_coordinate(txt)


def parse_coordinate(txt):
    """Parse coordinates like 'B2', '2B', 'b2' and return (row, col) or None.
    Row is 0-based (0..8), Col is 0-based (0..8).
    """
    if not isinstance(txt, str):
        return None
    s = txt.strip().upper()
    if len(s) != 2:
        return None

    a, b = s[0], s[1]
    letter = None
    number = None
    if 'A' <= a <= 'I' and '1' <= b <= '9':
        letter = a
        number = b
    elif 'A' <= b <= 'I' and '1' <= a <= '9':
        letter = b
        number = a
    else:
        return None

    row = ord(number) - ord('1')
    col = ord(letter) - ord('A')
    return row, col


def is_valid_number(txt):
    """Return integer 1..9 if valid, otherwise None."""
    if not isinstance(txt, str):
        return None
    s = txt.strip()
    if not s.isdigit():
        return None
    n = int(s)
    if 1 <= n <= 9:
        return n
    return None


def can_place(bd, r, c, n):
    """Check rules: empty cell, unique in row, column, and 3x3 box.
    Returns (True, None) if placeable, otherwise (False, reason_string).
    """
    if bd[r][c] != 0:
        return False, "Square already filled"
    if any(bd[r][j] == n for j in range(9)):
        return False, "Number already in that row"
    if any(bd[i][c] == n for i in range(9)):
        return False, "Number already in that column"
    br = (r // 3) * 3
    bc = (c // 3) * 3
    for i in range(br, br + 3):
        for j in range(bc, bc + 3):
            if bd[i][j] == n:
                return False, "Number already in that 3x3 box"

    return True, None


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
        raw = input("Specify a coordinate to edit or 'Q' to save and quit\n> ").strip()

        if raw.upper() == "Q":
            try:
                save_board(fn, bd)
                print(f"Saved board to {make_path(fn)}")
            except OSError as err:
                print(f"Save failed: {err}")
            keep = False
        else:
            spot = get_cell(raw)
            if spot is None:
                print("Please use a coordinate like B8 or Q to quit.")
            else:
                r, c = spot
                spot_name = f"{chr(ord('A')+c)}{r+1}"
                numtxt = input(f"What number goes in {spot_name}? ").strip()
                n = is_valid_number(numtxt)
                if n is None:
                    print("Please enter a number from 1 to 9.")
                else:
                    ok, reason = can_place(bd, r, c, n)
                    if not ok:
                        print(reason)
                    else:
                        bd[r][c] = n

        print()


if __name__ == "__main__":
    main()
