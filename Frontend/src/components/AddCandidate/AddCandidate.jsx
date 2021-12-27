import React, { useState } from 'react'
import { useForm, FormProvider } from 'react-hook-form'
import { Link } from 'react-router-dom'
import backend from '../../api/backend'
import './addCandidate.css'

const AddCandidate = () => {
    const { register, handleSubmit } = useForm();
    const [candidateSkills, setcandidateSkills] = useState([{ skill: '' }]);
    const [confirm, setConfirm] = useState(false);
    const [candidate, setCandidate] = useState({});

    const add = async (item) => {
        const response = await backend.post(`/jobcandidate/add/`,item);
    }

    const pom = (data) => {
        //pretvara niz objekata u niz stringova
        var names = data.candidateSkills.map(function (item) {
            return item['skill'];
        });

        data.candidateSkills=names;    
        add(data);
        setConfirm(true);
    }

    const handleInputChange = (e, index) => {
        //e.target je event tj ono sto si ukucao kao novi skill
        //name je skill: i u to ubacujes value, a index je brojac i
        const { name, value } = e.target;
        const list = [...candidateSkills];
        list[index][name] = value;
        setcandidateSkills(list);
    }

    const handleRemoveClick = index => {
        const list = [...candidateSkills];
        //splice brise element iz niza, stranica se opet renderuje i ono sto si obrisao nestane
        list.splice(index, 1);
        setcandidateSkills(list);
    }

    const handleAddClick = () => {
        setcandidateSkills([...candidateSkills, { skill: '' }]);
    }

    return (
        <div>
            {confirm
                ? <div className="confirmation" style={{ marginTop: '100px', textAlign: 'center' }}>
                    <h4>Job Candidate was added successfully</h4>
                    <Link to='/'>Back to Home</Link>
                </div>
                : <FormProvider>
                    <form className="addCandidates" onSubmit={handleSubmit((data) => pom({ ...data, candidateSkills }))}>
                        <div className='inputs'>
                            <label>Full Name: <input {...register('name')} /></label>
                            <label>Date of Birth: <input type='date' {...register('dateofbirth')} /></label>
                            <label>Contact Number: <input type='tel' {...register('contactnumber')} /></label>
                            <label>Email: <input type='email' {...register('email')} /></label>
                        </div>

                        {candidateSkills.map((x, i) => {
                            return (
                                <div className="skills" key={i}>
                                    <h4>candidateSkills</h4>
                                    <div className="box">
                                        <input
                                            type="text"
                                            name='skill'
                                            value={x.skill}
                                            placeholder="Enter Skill"
                                            onChange={e => handleInputChange(e, i)}
                                        />
                                        <div className="btnBox">
                                            {candidateSkills.length !== 1 &&
                                                <i
                                                    className="far fa-trash-alt delete"
                                                    onClick={() => handleRemoveClick(i)}
                                                />}
                                            {candidateSkills.length - 1 === i &&
                                                <i
                                                    className="fas fa-plus plus"
                                                    onClick={() => handleAddClick()}
                                                />}
                                        </div>
                                    </div>
                                </div>
                            )
                        })}

                        <button type='submit' className='submit'>Submit</button>
                    </form>
                </FormProvider>}
        </div>
    )
}

export default AddCandidate
