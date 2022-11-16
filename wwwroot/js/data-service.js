class DataService {
    static fetchJson(path) {
        return fetch("api/"+path).then((response) => {
            if( !response.ok )
                throw response.statusText;
            return response.json();
        }); 
    }
}
export default DataService;