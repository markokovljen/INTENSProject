import React from 'react'
import { BrowserRouter, Routes, Route } from 'react-router-dom'

import AddCandidate from './components/AddCandidate/AddCandidate'
import JobCandidates from './components/JobCandidates/JobCandidates'
import Navbar from './components/Navbar/Navbar'
import Search from './components/Search/Search'
import Details from './components/Details/Details'
import UpdateCandidate from './components/UpdateCandidate/UpdateCandidate'

const App = () => {
    return (
        <BrowserRouter>
            <Navbar />
            <div>
                <Routes>
                    <Route path='/' element={<><Search /><JobCandidates /></>} />        
                    <Route path='/addCandidate' element={<AddCandidate />} />
                    <Route path='/details/:id' element={<Details />} />
                    <Route path='/update/:id' element={<UpdateCandidate/>} />
                </Routes>
            </div>
        </BrowserRouter>
    )
}

export default App
