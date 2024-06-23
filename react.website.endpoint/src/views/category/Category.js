import React from 'react';
import { CTable, CTableHead, CTableRow, CTableHeaderCell, CTableDataCell, CTableBody, CButton, CSpinner } from '@coreui/react';
import axios from 'axios';

const Category = () => {
  const [categories, setCategories] = React.useState([]);
  React.useEffect(() => {
    axios({
      mode: 'no-cors',
      headers: { 'Content-Type': 'application/json' },
      url: 'http://localhost:5168/api/v1/Product/Type', // Change the URL as needed
    })
      .then(function (response) {
        setCategories(response.data.data.data);
      })
      .catch(function (error) {
        console.log(error);
      });
  }, []); // Empty dependency array to run once on component mount

  const showChildren = (id) => {
    window.location.hash = "#/Category/?parentId="+id;  // this reloads
    axios({
      mode: 'no-cors',
      headers: { 'Content-Type': 'application/json' },
      url: `http://localhost:5168/api/v1/Product/Type?parentId=${id}`,
    })
      .then(function (response) {
        setCategories(response.data.data.data);
      })
      .catch(function (error) {
        console.log(error);
      });
  };

  return (
    <>
      {categories.length > 0 ? (
                <div style={{overflowX: 'auto'}}>

        <CTable>
          <CTableHead>
            <CTableRow>
              <CTableHeaderCell scope="col">#</CTableHeaderCell>
              <CTableHeaderCell scope="col">Type</CTableHeaderCell>
              <CTableHeaderCell scope="col">Show Children</CTableHeaderCell>
            </CTableRow>
          </CTableHead>
          <CTableBody>
            {categories.map((object) => (
              <CTableRow key={object.id}>
                <CTableHeaderCell scope="row">{object.id}</CTableHeaderCell>
                <CTableDataCell>{object.type}</CTableDataCell>
                <CTableDataCell>
                <CButton color="primary" onClick={() => showChildren(object.id)}>
                    Show Children
                </CButton>
                </CTableDataCell>
              </CTableRow>
            ))}
          </CTableBody>
        </CTable>
        </div>
      ) : (
<div className="text-center">
  <CSpinner size="sm" variant="grow" style={{ marginTop: "25%", width: '4rem', height: '4rem' }} />
</div>      
)}
    </>
  );
};

export default Category;
