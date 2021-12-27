import React, {useState, useEffect} from 'react'
import './details.css'
import backend from '../../api/backend'

const Details = () => {
    const [candidate, setCandidate] = useState({});
    const id = window.location.pathname.substring(window.location.pathname.lastIndexOf('/') + 1);
    
    const fetchCandidate = async (id) => {
        const {data} = await backend.get(`/jobcandidate/detail/${id}`)
        setCandidate(data);
    }

    useEffect(() => {
        fetchCandidate(id);
    }, []);


   
    return (
        <div className="details">
            <h4 className="candidateName">{candidate.name}</h4>
            <div className="detail">
                Contact Number: <span>{candidate.contactNumber}</span>
            </div>
            <div className="detail">
                Email: <span>{candidate.email}</span>
            </div>
            <div className="detail">
                Skills: <span>{candidate.candidateSkills}</span>
            </div>
        </div>
    )
}

export default Details