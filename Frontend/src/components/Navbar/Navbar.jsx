import React from 'react'
import { Link } from 'react-router-dom'
import './navbar.css'

const Navbar = () => {
    return (
        <div className="navbar">
            <Link to='/' className='link-listItem'>
                Home
            </Link>
            <ul className="list">
                <li className="listItem">
                    <Link to='/addCandidate' className='link-listItem'>
                        Add Candidate
                    </Link>
                </li>
            </ul>
        </div>
    )
}

export default Navbar
