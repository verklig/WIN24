const questions = document.querySelectorAll(".question");

questions.forEach(question => {
  question.addEventListener("click", e => {
    const parent = question.parentElement;

    if(parent.classList.contains("open")) {
      parent.classList.remove("open");

      return;
    }

    document.querySelectorAll(".list-item").forEach(listItem => {
      listItem.classList.remove("open");
    })

    parent.classList.toggle("open");
  }
  )
})