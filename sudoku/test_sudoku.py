import unittest
from sudoku import parse_coordinate, is_valid_number, mk_board, can_place

class TestSudokuParsing(unittest.TestCase):
    def test_simple(self):
        self.assertEqual(parse_coordinate('B2'), (1, 1))
    def test_reversed(self):
        self.assertEqual(parse_coordinate('2B'), (1, 1))
    def test_lowercase(self):
        self.assertEqual(parse_coordinate('b2'), (1, 1))
    def test_invalid(self):
        self.assertIsNone(parse_coordinate('Z9'))
    def test_too_long(self):
        self.assertIsNone(parse_coordinate('A10'))

class TestNumberValidation(unittest.TestCase):
    def test_valid(self):
        self.assertEqual(is_valid_number('5'), 5)
    def test_invalid_zero(self):
        self.assertIsNone(is_valid_number('0'))
    def test_invalid_high(self):
        self.assertIsNone(is_valid_number('11'))
    def test_non_numeric(self):
        self.assertIsNone(is_valid_number('x'))

class TestPlacementRules(unittest.TestCase):
    def setUp(self):
        self.bd = mk_board()
        # set some numbers
        self.bd[0][0] = 5  # A1
        self.bd[0][1] = 3  # B1
        self.bd[1][0] = 6  # A2

    def test_square_filled(self):
        ok, reason = can_place(self.bd, 0, 0, 2)
        self.assertFalse(ok)
        self.assertIn('filled', reason.lower())

    def test_row_conflict(self):
        ok, reason = can_place(self.bd, 0, 2, 3)  # row 0 already has 3
        self.assertFalse(ok)
        self.assertIn('row', reason.lower())

    def test_col_conflict(self):
        ok, reason = can_place(self.bd, 2, 0, 5)  # column 0 already has 5
        self.assertFalse(ok)
        self.assertIn('column', reason.lower())

    def test_box_conflict(self):
        # place 3x3 box conflict: box (0,0) contains 5 and 3 and 6
        ok, reason = can_place(self.bd, 2, 2, 6)
        self.assertFalse(ok)
        self.assertIn('box', reason.lower())

    def test_ok_place(self):
        ok, reason = can_place(self.bd, 2, 2, 4)
        self.assertTrue(ok)
        self.assertIsNone(reason)

if __name__ == '__main__':
    unittest.main()
