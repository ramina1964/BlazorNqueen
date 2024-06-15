export function setQueen(col, row) {
  var queenImage = document.getElementById('queen');
  if (!queenImage) {
    console.error('Queen image not found!');
    return;
  }
  var clonedQueenImage = queenImage.cloneNode(true);
  clonedQueenImage.style.display = 'block'; // Make sure the image is visible
  var targetSquare = document.getElementById('square-' + row + '-' + col);
  if (!targetSquare) {
    console.error('Target square not found!');
    return;
  }

  // Clear the target square before placing the queen
  targetSquare.innerHTML = '';
  targetSquare.appendChild(clonedQueenImage);
}

export function clearNBoard() {
  var squares = document.querySelectorAll('.square');
  // Clear all squares on the board  
  squares.forEach(function (square) {
    square.innerHTML = '';
  });
}

export function redrawLabels() {
  var squares = document.querySelectorAll('.square');

  squares.forEach(function (square) {
    // Remove existing span if any
    var span = square.querySelector('span');
    if (span) {
      span.remove();
    }

    // Extract row and col from the square's id
    var idParts = square.id.split('-');
    var row = idParts[1];
    var col = idParts[2];

    // Create new span and add it to the square
    var newSpan = document.createElement('span');
    newSpan.style.color = 'black';
    newSpan.style.margin = '2px';
    newSpan.innerText = col + '-' + row;
    square.appendChild(newSpan);
  });
}
