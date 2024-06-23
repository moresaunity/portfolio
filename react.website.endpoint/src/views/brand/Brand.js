import React from 'react'
import { CTable, CTableHead, CTableRow, CTableHeaderCell, CTableDataCell, CTableBody, CButton, CSpinner } from '@coreui/react';
import axios from "axios";

const Brand = () => {

    const [brands, setBrands] = React.useState([]);

  React.useEffect(() => {
    axios({
            mode: 'no-cors',
            headers: { 'Content-Type': 'application/json'},
             url: 'http://localhost:5168/api/v1/Product/Brand',
          })
        .then(function (response) {
            setBrands(response.data.data);
        })
        .catch(function (error) {
            console.log(error);
        });
});
return (
    <>

      {brands.length > 0 ? (
            <div style={{overflowX: 'auto'}}>
      <CTable>
      <CTableHead>
        <CTableRow>
          <CTableHeaderCell scope="col">#</CTableHeaderCell>
          <CTableHeaderCell scope="col">Name</CTableHeaderCell>
        </CTableRow>
      </CTableHead>
      <CTableBody>
        {brands.map((object, i) => (
          <CTableRow key={object.id}>
            <CTableHeaderCell scope="row">{object.id}</CTableHeaderCell>
            <CTableDataCell>{object.brand}</CTableDataCell>
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

export default Brand
