// Store a reference to the root list element
const todoList = document.getElementById("todo-list")


// Fetch data and pass them forward to the create function
fetch("/todoes")
  .then(response => response.json())
  .then(data => createTodos(data))


/**
 * The shape of the JSON object containing the todo data
 * 
 * @typedef {{
 * Id: number
 * Name?: string
 * IsComplete: boolean
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

  if(!Array.isArray(todosData)) {
    throw new Error("Todo data is not an array")
  }

  const todoes = todosData.map(todo => {
    // Setup the Todo "Card"
    const todoCard = document.createElement("div")

    // Create the title and append to container
    const title = document.createElement("h2")
    title.textContent = todo.Name
    todoCard.appendChild(title)

    const complete = document.createElement("h2")
    complete.textContent = todo.IsComplete ? "true": "false"
    todoCard.appendChild(complete)

    return todoCard
  })
  
  batchAttach(todoes, attachPoint)
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
  const fragment = document.createDocumentFragment()

  elements.forEach(element => fragment.appendChild(element))

  attachPoint.appendChild(fragment)
}