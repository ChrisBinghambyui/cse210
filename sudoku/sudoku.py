import json
import os
from dataclasses import dataclass, field
from typing import List, Optional, Tuple


BoardRows = List[List[int]]


@dataclass
class BoardData:
    cells: BoardRows = field(default_factory=lambda: [[0 for _ in range(9)] for _ in range(9)])

    @classmethod
    def from_rows(cls, rows: BoardRows) -> "BoardData":
        if len(rows) != 9:
            raise ValueError("Board must have exactly 9 rows.")

        board = cls()
        for row_index in range(9):
            row = rows[row_index]
            if len(row) != 9:
                raise ValueError("Each row must have exactly 9 values.")

            for col_index in range(9):
                value = row[col_index]
                if not isinstance(value, int) or value < 0 or value > 9:
                    raise ValueError("Board values must be integers between 0 and 9.")
                board.cells[row_index][col_index] = value

        return board

    def to_rows(self) -> BoardRows:
        return [row[:] for row in self.cells]

    def get_value(self, row: int, col: int) -> int:
        return self.cells[row][col]

    def set_value(self, row: int, col: int, value: int) -> None:
        self.cells[row][col] = value


class SudokuStorage:
    def load(self, filename: str) -> BoardData:
        with open(filename, "r", encoding="utf-8") as source:
            data = json.load(source)

        if isinstance(data, dict):
            rows = data.get("board")
        else:
            rows = data

        if rows is None:
            raise ValueError("File did not contain a board.")

        return BoardData.from_rows(rows)

    def save(self, filename: str, board: BoardData) -> None:
        directory = os.path.dirname(filename)
        if directory:
            os.makedirs(directory, exist_ok=True)

        with open(filename, "w", encoding="utf-8") as target:
            json.dump({"board": board.to_rows()}, target, indent=2)


class SudokuGame:
    def __init__(self) -> None:
        self.storage = SudokuStorage()
        self.board = BoardData()
        self.base_dir = os.path.dirname(os.path.abspath(__file__))
        self.current_filename = "131.05.Easy.json"

    def resolve_filename(self, filename: str) -> str:
        if os.path.isabs(filename):
            return filename

        return os.path.join(self.base_dir, filename)

    def run(self) -> None:
        print("Sudoku")
        self.load_board()

        keep_running = True
        while keep_running:
            self.display_board_state()

            command = input("Specify a coordinate to edit or 'Q' to save and quit\n> ").strip().upper()
            if command == "Q":
                self.save_board()
                keep_running = False
            else:
                self.edit_coordinate(command)
            print()

    def load_board(self) -> None:
        filename = input(f"Enter board filename to load [{self.current_filename}]: ").strip()
        if filename == "":
            filename = self.current_filename

        try:
            self.board = self.storage.load(self.resolve_filename(filename))
            self.current_filename = filename
        except (OSError, ValueError, json.JSONDecodeError) as exc:
            print(f"Load failed: {exc}")

    def display_board_state(self) -> None:
        print("   A B C D E F G H I")

        for row in range(9):
            row_label = str(row + 1)
            row_values: List[str] = []
            for col in range(9):
                value = self.board.get_value(row, col)
                row_values.append(" " if value == 0 else str(value))

            left = f"{row_label}  "
            first = " ".join(row_values[0:3])
            second = " ".join(row_values[3:6])
            third = " ".join(row_values[6:9])
            print(f"{left}{first}|{second}|{third}")

            if row in (2, 5):
                print("   -----+-----+-----")

    def edit_coordinate(self, coordinate: str) -> None:
        row_col = self.parse_coordinate(coordinate)
        if row_col is None:
            print("Please use a coordinate like B8 or Q to quit.")
            return

        row, col = row_col
        target_name = f"{chr(ord('A') + row)}{col + 1}"
        entry_text = input(f"What number goes in {target_name}? ").strip()

        if not entry_text.isdigit():
            print("Please enter a number from 0 to 9.")
            return

        entry = int(entry_text)
        if entry < 0 or entry > 9:
            print("Please enter a number from 0 to 9.")
            return

        self.board.set_value(row, col, entry)

    def parse_coordinate(self, coordinate: str) -> Optional[Tuple[int, int]]:
        if len(coordinate) != 2:
            return None

        row_char = coordinate[0]
        col_char = coordinate[1]

        if not ("A" <= row_char <= "I" and "1" <= col_char <= "9"):
            return None

        return ord(row_char) - ord("A"), ord(col_char) - ord("1")

    def save_board(self) -> None:
        try:
            path = self.resolve_filename(self.current_filename)
            self.storage.save(path, self.board)
            print(f"Saved board to {path}")
        except OSError as exc:
            print(f"Save failed: {exc}")


if __name__ == "__main__":
    SudokuGame().run()
