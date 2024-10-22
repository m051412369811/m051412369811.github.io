const calendar = document.querySelector(".calendar"),
    date = document.querySelector(".date"),
    daysContainer = document.querySelector(".days"),
    prev = document.querySelector(".prev"),
    next = document.querySelector(".next"),
    todayBtn = document.querySelector(".today-btn");
let selectedDateID ="";


let activeDay;
let today = new Date();
let month = today.getMonth(); 
let year = today.getFullYear();

const months = [
    "January",
    "February",
    "March",
    "April",
    "May",
    "June",
    "July",
    "August",
    "September",
    "October",
    "November",
    "December",
];


function initCalendar() {
    const firstDay = new Date(year, month, 1);
    const lastDay = new Date(year, month + 1, 0);
    const prevLastDay = new Date(year, month, 0);
    const prevDays = prevLastDay.getDate();
    const lastDate = lastDay.getDate();
    const day = firstDay.getDay();
    const nextDays = 7 - lastDay.getDay() - 1;


    date.innerHTML = months[month] + " " + year;

    let days = "";

    for (let x = day; x > 0; x--) {
        days += `<div class="day prev-date">${prevDays - x + 1}</div>`;
    }

    for (let i = 1; i <= lastDate; i++) {
        if (
            i === new Date().getDate() &&
            year === new Date().getFullYear() &&
            month === new Date().getMonth()
        ) {
            days += `<div class="day today" id="${year}/${month+1}/${i}" data-bs-toggle="modal" data-bs-target="#staticBackdrop">${i}</div>`;
        } else {
            days += `<div class="day" id="${year}/${month+1}/${i}" data-bs-toggle="modal" data-bs-target="#staticBackdrop">${i}</div>`;
        }
    }

    for (let j = 1; j <= nextDays; j++) {
        days += `<div class="day next-date">${j}</div>`;
    }
    daysContainer.innerHTML = days;
    }

    function prevMonth() {
    month--;
    if (month < 0) {
        month = 11;
        year--;
    }
    initCalendar();
    }

    function nextMonth() {
    month++;
    if (month > 11) {
        month = 0;
        year++;
    }
    initCalendar();
}

prev.addEventListener("click", prevMonth);
next.addEventListener("click", nextMonth);

initCalendar();

todayBtn.addEventListener("click", () => {
    today = new Date();
    month = today.getMonth();
    year = today.getFullYear();
    initCalendar();
});

function modalTittle(){
    const modalTry = document.querySelector("#staticBackdropLabel");
    const el= document.querySelector("div");
    const modaleName = el.onclick = function(e){  
        modalTry.innerHTML = e.target.id;  
        }
}
modalTittle();

daysContainer.addEventListener('click', function(event) {
  if (event.target.classList.contains('day')) {
      selectedDateID = event.target.id;
      console.log(`點擊的日期 id:${selectedDateID}`);
  };
  //要把當天的代辦事項放上去modal

});




//點擊日期的時候要出現當天的行事曆(可以使用onclick功能設置在html裡面)

//行事曆要有關閉的按鈕

//當天的行事曆內容要出現在當天的行程(從localStorage抓出來)

const todoInput = document.querySelector("#todo_input");
const addButton = document.querySelector("#add_btn");
const key = "todoList";
const todoListGroup = document.querySelector("#todo_list_group");
const todoItemTemplate = document.querySelector("#todo_item_template");

function createTodoItemEl(todoItem) {
    const todoItemEl = todoItemTemplate.content
        .querySelector(".list-group-item")
        .cloneNode(true);
        todoItemEl.dataset.id = todoItem.id;
        todoItemEl.querySelector(".is-done-input").checked = todoItem.isDone;
        todoItemEl.querySelector(".todo-item-content").value = todoItem.content;
        todoItemEl.addEventListener("click", handleTodoItemClick);
    return todoItemEl;
}

function handleTodoItemClick(e) {
    console.dir(e.target);
    const targetEl = e.target;
    const todoItemIdStr = targetEl.closest("[data-id]").dataset.id;
    const todoItemId = parseInt(todoItemIdStr);
    console.log(todoItemId);

    if (targetEl.classList.contains("edit-btn")) {
            edit(todoItemId, targetEl);
    } else if (targetEl.classList.contains("save-btn")) {
            save(todoItemId, targetEl);
    } else if (targetEl.classList.contains("remove-btn")) {
            remove(todoItemId, targetEl);
    } else if (targetEl.classList.contains("is-done-input")) {
            isDoneCheck(todoItemId, targetEl);
    }
}

addButton.addEventListener("click", function (event) {
    const todoContent = todoInput.value.trim();

    if (!todoContent) return;
    // 儲存待辦事項
    const todoItem = {
        date : selectedDateID,
        id: new Date().valueOf(),
        content: todoContent,
        isDone: false,
    };
    saveTodoItem(todoItem);
    // 生成TODO 的 HTML
    renderingTodoList();
});
window.addEventListener("load", function (event) {
    renderingTodoList();
});

function renderingTodoList() {
    const todoList = getTodoListFromStorage();
    if (!todoList) return
    todoListGroup.innerHTML = "";
    //在這邊要加入如果id相同的才會被顯示在上面
    todoList.forEach((item) => {
        // todoListGroup.innerHTML += createTodoItemHTML(item);
        //用 todoListGroup.append() 的方式加入DOM
        const todoItemDOM = createTodoItemEl(item);
        todoListGroup.append(todoItemDOM);
    });
}
      function getTodoListFromStorage() {
        //取得現在所有的todoItem,再加上去
        const localStorageItem = localStorage.getItem(key);
        return localStorageItem ? JSON.parse(localStorageItem) : [];
      }
      function createTodoItemHTML(todoItem) {
        return `<li class="list-group-item">
                  <div class="input-group" data-id="${todoItem.id}">
                    <div class="input-group-text">
                      <input
                        class="form-check-input mt-0"
                        type="checkbox"
                        oninput="isDoneCheck(${todoItem.id})"
                        ${todoItem.isDone ? "checked" : ""}
                      />
                    </div>
                    <input
                      type="text"
                      class="form-control todo-content"
                      aria-label="Text input with checkbox"
                      value="${todoItem.content}"
                      disabled
                    />
                    <button class="btn btn-success save-btn d-none" type="button" onclick="save(${
                      todoItem.id
                    }, this)">儲存</button>
                    <button class="btn btn-warning" type="button" onclick="edit(${
                      todoItem.id
                    }, this)">編輯</button>
                    <button class="btn btn-danger" type="button" onclick="remove(${
                      todoItem.id
                    }, this)">刪除</button>
                  </div>
                </li>`;
      }
      function isDoneCheck(id) {
        const todoList = getTodoListFromStorage();
        const todoItem = todoList.find((item) => item.id === id);
        todoItem.isDone = !todoItem.isDone;
        saveTodoListToStorage(todoList);
      }
      function save(id, el) {
        const todoContent = el.parentElement.querySelector(".todo-content");
        const val = todoContent.value.trim();
        if (!val) return;

        const todoList = getTodoListFromStorage();

        const todoItem = todoList.find((item) => item.id === id);
        todoItem.content = val;
        saveTodoListToStorage(todoList);
        renderingTodoList();
      }
      function saveTodoListToStorage(todoList) {
        const json = JSON.stringify(todoList);
        localStorage.setItem(key, json);
      }
      function edit(id, el) {
        const todoContent = el.parentElement.querySelector(".todo-content");
        todoContent.disabled = false;
        const saveBtn = el.parentElement.querySelector(".save-btn");
        saveBtn.classList.remove("d-none");
        el.classList.add("d-none");
      }
      function remove(id, el) {
        const todoList = getTodoListFromStorage();
        const todoItemIdx = todoList.findIndex((item) => item.id === id);
        todoList.splice(todoItemIdx, 1);
        saveTodoListToStorage(todoList);
        renderingTodoList();
      }
      function saveTodoItem(todoItem) {
        //取得現在所有的todoItem,再加上去
        const todoList = getTodoListFromStorage();
        todoList.push(todoItem);
        saveTodoListToStorage(todoList);
      }


