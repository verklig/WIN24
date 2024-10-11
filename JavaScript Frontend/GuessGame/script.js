let randomNumber = 0;
let numOfTries = 0;

document.getElementById("btn").addEventListener("click", readInput);

resetGame();

function readInput() {
  let userInput = document.querySelector("#input").value;
  let result = document.querySelector("#result");
  let ok = checkValue(userInput);

  if (ok) {
    numOfTries++;
    checkWin(userInput, result);
  }
}

function checkValue(userInput) {
  if (userInput < 1 || userInput > 100) {
    alert("Input must be between 1 and 100!");
    return false;
  }
  else if (isNaN(userInput)) {
    alert("Input must be a number!");
    return false;
  }
  
  return true;
}

function checkWin(userInput, result) {
  if (userInput == randomNumber) {
    if (numOfTries == 1) {
      result.innerText = "You guessed right! The number was " + randomNumber + ", it took you " + numOfTries + " try.";
    }
    else {
      result.innerText = "You guessed right! The number was " + randomNumber + ", it took you " + numOfTries + " tries.";
    }

    resetGame();
  }
  else if (userInput > randomNumber) {
    result.innerText = "The number is lower than " + userInput;
  }
  else if (userInput < randomNumber) {
    result.innerText = "The number is higher than " + userInput;
  }
}

function resetGame() {
  randomNumber = Math.floor(Math.random() * 100) + 1;
  numOfTries = 0;
}