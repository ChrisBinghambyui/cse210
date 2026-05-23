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

    def is_placement_valid(self, row: int, col: int, entry: int) -> bool:
        if entry == 0:
            return True

        for index in range(9):
            if index != col and self.cells[row][index] == entry:
                return False
            if index != row and self.cells[index][col] == entry:
                return False

        start_row = (row // 3) * 3
        start_col = (col // 3) * 3
        for r in range(start_row, start_row + 3):
            for c in range(start_col, start_col + 3):
                if (r != row or c != col) and self.cells[r][c] == entry:
                    return False

        return True

    def is_solved(self) -> bool:
        for row in range(9):
            for col in range(9):
                value = self.cells[row][col]
                if value == 0:
                    return False
                if not self.is_placement_valid(row, col, value):
                    return False
        return True


class SudokuStorage:
    def load(self, filename: str) -> BoardData:
        with open(filename, "r", encoding="utf-8") as source:
            rows = json.load(source)
        return BoardData.from_rows(rows)

    def save(self, filename: str, board: BoardData) -> None:
        directory = os.path.dirname(filename)
        if directory:
            os.makedirs(directory, exist_ok=True)

        with open(filename, "w", encoding="utf-8") as target:
            json.dump(board.to_rows(), target, indent=2)


class SudokuGame:
    def __init__(self) -> None:
        self.storage = SudokuStorage()
        self.board = BoardData()
        self.current_filename = "sudoku/board.json"

    def run(self) -> None:
        print("Sudoku")
        print("Commands: L=Load, S=Save, E=Edit Cell, D=Display, Q=Quit")
        print()

        keep_running = True
        while keep_running:
            self.display_board_state()

            if self.board.is_solved():
                print("Board is solved. Nice work!")

            command = input("Choose command (L/S/E/D/Q): ").strip().upper()

            if command == "L":
                self.handle_load()
            elif command == "S":
                self.handle_save()
            elif command == "E":
                self.entry_and_validate()
            elif command == "D":
                pass
            elif command == "Q":
                keep_running = False
            else:
                print("Unknown command. Try L, S, E, D, or Q.")

            print()

    def handle_load(self) -> None:
        filename = input("Enter board filename to load (default sudoku/board.json): ").strip()
        if filename == "":
            filename = self.current_filename

        try:
            self.board = self.storage.load(filename)
            self.current_filename = filename
            print(f"Loaded board from {os.path.abspath(filename)}")
        except (OSError, ValueError, json.JSONDecodeError) as exc:
            print(f"Load failed: {exc}")

    def handle_save(self) -> None:
        filename = input("Enter board filename to save (default sudoku/board.json): ").strip()
        if filename == "":
            filename = self.current_filename

        try:
            self.storage.save(filename, self.board)
            self.current_filename = filename
            print(f"Saved board to {os.path.abspath(filename)}")
        except OSError as exc:
            print(f"Save failed: {exc}")

    def display_board_state(self) -> None:
        print("    1 2 3   4 5 6   7 8 9")
        print("  +-------+-------+-------+")

        for row in range(9):
            row_label = chr(ord("A") + row)
            print(f"{row_label} | ", end="")
            for col in range(9):
                value = self.board.get_value(row, col)
                text = "." if value == 0 else str(value)
                print(f"{text} ", end="")
                if (col + 1) % 3 == 0:
                    print("| ", end="")
            print()
            if (row + 1) % 3 == 0:
                print("  +-------+-------+-------+")

    def entry_and_validate(self) -> None:
        target = self.get_target()
        if target is None:
            return

        row, col = target

        current_value = self.board.get_value(row, col)
        if current_value != 0:
            overwrite = input(f"Current value is {current_value}. Overwrite? (y/n): ").strip().lower()
            if overwrite != "y":
                return

        entry = self.get_entry_value()
        if entry is None:
            return

        previous_value = self.board.get_value(row, col)
        self.board.set_value(row, col, entry)

        if not self.board.is_placement_valid(row, col, entry):
            self.board.set_value(row, col, previous_value)
            print("That entry violates Sudoku rules. Change canceled.")
            return

        print("Entry accepted.")

    def get_target(self) -> Optional[Tuple[int, int]]:
        max_attempts = 3
        attempts = 0

        while attempts < max_attempts:
            target_text = input("Enter target cell (like B5): ").strip().upper()
            attempts += 1

            if len(target_text) == 2:
                row_char = target_text[0]
                col_char = target_text[1]

                if "A" <= row_char <= "I" and "1" <= col_char <= "9":
                    row = ord(row_char) - ord("A")
                    col = ord(col_char) - ord("1")
                    return row, col

            if attempts < max_attempts:
                print("Target must be A-I plus 1-9, for example B5.")

        print("Too many invalid targets. Entry canceled.")
        return None

    def get_entry_value(self) -> Optional[int]:
        max_attempts = 3
        attempts = 0

        while attempts < max_attempts:
            entry_text = input("Enter value 1-9 (or 0 to clear): ").strip()
            attempts += 1

            if entry_text.isdigit():
                entry = int(entry_text)
                if 0 <= entry <= 9:
                    return entry

            if attempts < max_attempts:
                print("Entry must be a number from 0 to 9.")

        print("Too many invalid entries. Entry canceled.")
        return None


if __name__ == "__main__":
    SudokuGame().run()
