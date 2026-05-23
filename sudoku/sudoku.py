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

    row = txt[0].upper()
    col = txt[1]
    if row < "A" or row > "I" or col < "1" or col > "9":
        return None

    return ord(row) - ord("A"), ord(col) - ord("1")


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
                num = input(f"What number goes in {cmd}? ").strip()
                if num.isdigit() and 0 <= int(num) <= 9:
                    bd[r][c] = int(num)
                else:
                    print("Please enter a number from 0 to 9.")

        print()


if __name__ == "__main__":
    main()
