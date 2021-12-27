import React, { useState, useEffect } from 'react'
import { traverseTwoPhase } from 'react-dom/cjs/react-dom-test-utils.production.min';
import { Link } from 'react-router-dom';
import backend from '../../api/backend'

const UpdateCandidate = () => {
    const [confirm, setConfirm] = useState(false);
    const [candidate, setCandidate] = useState(null);
    const [skills, setSkills] = useState([{ skill: '' }]);
    const [tempCandidate, setTempCandidate] = useState(false);

    const id = window.location.pathname.substring(window.location.pathname.lastIndexOf('/') + 1);
    var names = skills.map(function (item) {
        return item['skill'];
    });

    const fetchCandidate = async (id) => {
        const response = await  backend.get(`/jobcandidate/detail/${id}`)
        setCandidate(response.data);
        setTempCandidate(true);
        
    }
    

    const updateCandidateToDb = async (id, candidate) => {
        const response = await backend.put(`/jobcandidate/update/${id}`, candidate)
        
    }

    const removeCandidateSkill=async (id,skill)=>{
        
        const response = await backend.put(`/jobcandidate/remove/${id}/${skill}`)
    }

    useEffect(() => {
        fetchCandidate(id);
        
    }, []);

    useEffect(() => {
        try{
            setSkills(candidate.candidateSkills.map((str, index) => ({ skill: str, id: index + 1 })))
        }
        catch{
            console.log("property is null")
        }
        
    }, [tempCandidate]);

    const handleChange = e => {
        const { name, value } = e.target;
        setCandidate(prevState => ({
            ...prevState,
            [name]: value
        }));
    };

    const handleInputChange = (e, index) => {
        const { name, value } = e.target;
        const list = [...skills];
        list[index][name] = value;
        setSkills(list);

        setCandidate(prevState => ({
            ...prevState,
            ["candidateSkills"]: names
        }));
    }

    const handleRemoveClick = skill => {
        removeCandidateSkill(id,skill)
        setConfirm(true)
    }

    const handleAddClick = () => {
        setSkills([...skills, { skill: '' }]);
    }

    const submitForm = () => {

        updateCandidateToDb(id, candidate);
        setConfirm(true);

    }


    const UpdateSkills = ()=>(
        <div style={{ marginTop: "100px" }}>
            
            {
                skills.map((x, i) => {
                return (
                    <div className="skills" key={i}>
                        <h4>Skills</h4>
                        <div className="box">
                            <input
                                type="text"
                                name='skill'
                                value={x.skill}
                                placeholder="Enter Skill"
                                onChange={e => handleInputChange(e, i)}
                            />
                            <div className="btnBox">
                                {skills.length !== 1 &&
                                    <i
                                        className="far fa-trash-alt delete"
                                        onClick={() => handleRemoveClick(x.skill)}
                                    />}
                                {skills.length - 1 === i &&
                                    <i
                                        className="fas fa-plus plus"
                                        onClick={() => handleAddClick()}
                                    />}
                            </div>
                        </div>
                    </div>
                )
            })}
        </div>
    )

    

    return (
        <div>
            {candidate 
                ? confirm
                    ? <div className="confirmation" style={{ marginTop: '100px', textAlign: 'center' }}>
                        <h4>Job Candidate was successfully updated</h4>
                        <Link to='/'>Back to Home</Link>
                    </div>
                    : <form className="addCandidates" onSubmit={() => submitForm()}>
                        <div className='inputs'>
                            <label>Full Name:
                                <input name='name' value={candidate.name} onChange={handleChange} />
                            </label>
                            <label>Contact Number:
                                <input type='tel' name='contactNumber' value={candidate.contactNumber} onChange={handleChange} />
                            </label>
                            <label>Email:
                                <input type='email' name='email' value={candidate.email} onChange={handleChange} />
                            </label>
                        </div>
                        <UpdateSkills/>
                        <button type='submls>it' className='submit'>Submit</button>
                    </form>
                : <h1>..Loading</h1>}
        </div>

    )

}

export default UpdateCandidate