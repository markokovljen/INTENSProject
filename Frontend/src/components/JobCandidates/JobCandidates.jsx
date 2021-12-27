import React, {useState, useEffect} from 'react'
import { Link } from 'react-router-dom'
import './jobCandidates.css'
import backend from '../../api/backend'




const JobCandidates = () => {
    const [candidates, setCandidates] = useState([]);
    const[deleted, setDeleted] = useState(false);

    const deleteCandidate = async (id) => {
        const response = await backend.delete(`/jobcandidate/delete/${id}`)
        
    }

    const handleDeleteCandidate = (id) => {       
        deleteCandidate(id);
        setDeleted(!deleted);
        
    }
    
    const fetchCandidates = async () => {
        const {data} = await backend.get('/jobcandidate/list');
        setCandidates(data);
    }

    useEffect(() => {
        fetchCandidates();
    }, [deleted])

    const renderedContent = candidates.map((item) => {
        return (
            <li key={item.id}>
                <h4 className='candidate'>{item.name}</h4>
                <div className="links">
                    <Link to={`/details/${item.id}`} className='link-details'>
                        Details
                    </Link>
                    <Link to={`/update/${item.id}`} className='link-details'>
                        Update
                    </Link>
                    <i className="far fa-trash-alt delete" onClick={() => handleDeleteCandidate(item.id)}></i>
                </div>
            </li>
        )
    })

    return (
        <div className='candidates'>
            <ul className="candidatesList">
                {renderedContent}
            </ul>
        </div>
    )
}

export default JobCandidates
