from reportlab.lib.pagesizes import letter
from reportlab.pdfgen import canvas

lines = [
    "Sudoku Coordinate Parsing Test Cases",
    "",
    "1) Name: Simple coordinate",
    "   Input: B2",
    "   Output: (1,1)",
    "   Type: Requirement",
    "",
    "2) Name: Reversed coordinate",
    "   Input: 2B",
    "   Output: (1,1)",
    "   Type: Requirement",
    "",
    "3) Name: Lowercase coordinate",
    "   Input: b2",
    "   Output: (1,1)",
    "   Type: Boundary",
    "",
    "4) Name: Invalid letter",
    "   Input: Z9",
    "   Output: None",
    "   Type: Error",
    "",
    "5) Name: Too long input",
    "   Input: A10",
    "   Output: None",
    "   Type: Error",
    "",
    "6) Name: Non-numeric number input",
    "   Input: place 'X' after coordinate",
    "   Output: reject as invalid number",
    "   Type: Error",
    "",
]

c = canvas.Canvas('sudoku/test_cases.pdf', pagesize=letter)
width, height = letter
x = 72
y = height - 72
c.setFont('Helvetica', 12)
for line in lines:
    c.drawString(x, y, line)
    y -= 14
c.save()
