import  React, { useState } from 'react';
function N1(){
const [ tasks, setTasks] = useState([]);
const [newTask,setNewTask] =useState("");

     function InputChange(event){
         setNewTask(event.target.value)
     }
     function add(){ setTasks([...tasks,newTask]);setNewTask("");
     }
     function Delete (index) { const update=tasks.filter((_ , i)=> i !==index);
     setTasks(update);
    }

      return(<div>
        <div className=' text-center mt-10'><br/>
            <h1 className='text-pink-950  text-2xl font-bold font-sans'><b>TO-DO-LIST</b></h1>
            <input type="text" placeholder='Enter your tasks' className='border  border-pink-950 mt-10 mb-2 rounded px-4 py-2 focus:ring-2 pr-8' value={newTask} onChange={InputChange}/>
<br/><button id="button" className=' border border-pink-950 pl-4 text-pink-950 rounded text-2l' onClick={add} >Add</button>
        </div> <br/>
             <ol className='text-center text-pink-950 '>
                 {tasks.map((task, index)=> <li key={index}  className='text-pink-950'>
                     <span>{task}</span>
                     <button id="button" className=' px-2 border rounded-3xl text-pink-950 text-sm' onClick={() => Delete(index)}>Delete</button>
                 </li>
                 )}
             </ol>
          </div>
    );}export default N1