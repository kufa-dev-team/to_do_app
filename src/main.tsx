import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import N1 from './n1.tsx'
import { BrowserRouter } from 'react-router-dom'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
      <BrowserRouter><N1/></BrowserRouter>


  </StrictMode>,
)
