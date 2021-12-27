import React, { useState } from 'react'
import backend from '../../api/backend';
import './search.css'

const Search = () => {
    const [name, setName] = useState('');
    const [skill, setSkill] = useState('');
    const [onSearch, setOnSearch] = useState(false);
    const [results, setResults] = useState([]);

    const searchRequest = async () => {
       
        const { data } = await backend.get(`/jobcandidate/search/${name}/${skill}`);
        setResults(data);
        setOnSearch(true);
    }   


    const ResultOfSearch = results.map((item) => {
        <div>
            <h2>Results of Search:</h2>
            <ul>
                <li>{item.name}</li>
            </ul>
        </div>
    })
    return (
        <div>
            {onSearch ? <ResultOfSearch /> : null}
            <form  className="search" onSubmit={() => searchRequest()}>
                <input type="text" className='name-search' placeholder='Enter name' onChange={(e) => setName(e.target.value)} />
                <input type="text" className='name-search' placeholder='Enter skill' onChange={(e) => setSkill(e.target.value)} />
                <button type="submit" className='submit'>Search</button>
            </form>
        </div>
    )
}

export default Search