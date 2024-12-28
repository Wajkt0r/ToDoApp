const API_URL = 'http://localhost:5000/api/tasks';

document.getElementById('addTaskBtn').addEventListener('click', async () => {
    const title = document.getElementById('taskTitle').value.trim();
    if (!title) return alert('Please enter a task title');
    
    try {
        const response = await fetch(API_URL, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ title })
        });

        if (response.ok) {
            document.getElementById('taskTitle').value = ''; // Clear the input field
            loadTasks();
        } else {
            alert('Failed to add task.');
        }
    } catch (error) {
        console.error('Error:', error);
    }
});

async function loadTasks() {
    try {
        const response = await fetch(API_URL);
        const tasks = await response.json();
        const list = document.getElementById('taskList');
        list.innerHTML = '';

        tasks.forEach(task => {
            const li = document.createElement('li');
            li.classList.add('task-item');
            
            const taskTitle = document.createElement('span');
            taskTitle.textContent = task.title;
            taskTitle.classList.add('task-title');

            const removeBtn = document.createElement('button');
            removeBtn.textContent = '❌';
            removeBtn.classList.add('remove-btn');
            removeBtn.addEventListener('click', () => removeTask(task.id));

            li.appendChild(taskTitle);
            li.appendChild(removeBtn);
            list.appendChild(li);
        });
    } catch (error) {
        console.error('Error loading tasks:', error);
    }
}

async function removeTask(taskId) {
    if (!confirm('Are you sure you want to delete this task?')) return;

    try {
        const response = await fetch(`${API_URL}/${taskId}`, {
            method: 'DELETE'
        });

        if (response.ok) {
            loadTasks();
        } else {
            alert('Failed to delete task.');
        }
    } catch (error) {
        console.error('Error:', error);
    }
}

// Initial load of tasks
loadTasks();
