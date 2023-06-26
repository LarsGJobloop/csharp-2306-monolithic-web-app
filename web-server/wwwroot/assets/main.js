// Store a reference to the root list element
const todoList = document.getElementById("todo-list")


// Fetch data and pass them to the todo create function
fetch("/todoes")
  .then(response => response.json())
  .then(data => createTodos(data.body))

/**
 * Sends a request to the backend for toggling
 * the todo status
 * 
 * @param {number} id 
 * Id of the todo
 * @param {*} currentStatus 
 * Current status of the Todo
 */ 
async function toggleComplete(id, currentStatus) {
  const response = await fetch("/todoitems/" + id, {
    method: "PUT",
    headers: new Headers({'content-type': 'application/json'}),
    // Set new information here
    body: {
      Id: id,
      Name: "Hello World",
      IsComplete: false,
    }
  });

  if(response.ok) {
    console.log(`Toggled status of todo: ${id}`)
  } else {
    console.log("Something went wrong")
  }
}


/**
 * The shape of the JSON object containing the todo data
 * 
 * @typedef {{
 * id: number
 * name?: string
 * isComplete: boolean
* }} Todo
*/

/**
 * Inserts a list of Todo elements into
 * the document.
 * 
 * @param {Todo[]} todosData
 * A list of todo data 
 */
function createTodos(todosData) {
  console.log(todosData)

  // Throw an error if we do not get an array back from
  // the Web API
  if(!Array.isArray(todosData)) {
    throw new Error("Todo data is not an array")
  }

  const todoes = todosData.map(todo => createTodoCard(todo))
  
  batchAttach(todoes, todoList)
}

/**
 * Attaches a group of HTML elements to the
 * attachPoint
 * 
 * @param {HTMLElement[]} elements 
 * elements to attach
 * @param {HTMLElement} attachPoint 
 * element to attach to
 */
function batchAttach(elements, attachPoint) {
  // This creates an in memory HTML element
  // which we append children to
  const fragment = document.createDocumentFragment()
  elements.forEach(element => fragment.appendChild(element))

  // This allows us to only update the content of the
  // web page once, causing only a single redraw
  // compared to updating the webbrowser
  // "number of elements to append" times
  attachPoint.appendChild(fragment)
}

/**
 * Creates a new todo card from the data
 * 
 * @param {Todo} todo
 * Todo details
 * 
 * @returns The new Todo Card
 */
function createTodoCard(todo) {
    // Setup the Todo "Card"
    const todoCard = document.createElement("div")
    todoCard.className = "todo-item"

    // Create the title and append to container
    const title = document.createElement("h2")
    title.textContent = todo.name
    todoCard.appendChild(title)

    const complete = document.createElement("h2")
    complete.textContent = todo.isComplete ? "true": "false"
    todoCard.appendChild(complete)


    // Add an event listner for toggling the status
    todoCard.addEventListener("click", () => toggleComplete(todo.id, todo.isComplete))

    return todoCard
}