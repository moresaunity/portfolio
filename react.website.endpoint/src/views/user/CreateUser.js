import { CForm, CCol, CFormInput, CFormCheck, CButton, css } from "@coreui/react";
import { RingLoader } from "react-spinners";

const CreateUser = () => {
  const [isLoading, setIsLoading] = useState(false);
  const override = css`
  display: block;
  margin: 0 auto;
`;
    function PostData(e) {
        e.preventDefault();
        setIsLoading(true); // نمایش لودینگ

        var model = {
          email: document.getElementById("email").value,
          phoneNumber: document.getElementById("phoneNumber").value,
          password: document.getElementById("password").value,
          isPersistent: false
        };
        if(document.getElementById("isPersistent").value == "on") model.isPersistent = true;
        console.log(model);
        // Simple POST request with a JSON body using fetch
              fetch("http://localhost:5168/api/v1/Account", {
                method: "POST",
                body: JSON.stringify(model),
                headers: {
                  "Content-type": "application/json; charset=UTF-8"
                }
              })
              .then((response) => response.json())
              .then((json) => {
                console.log(json);
                setIsLoading(false); // پایان نمایش لودینگ
              });
    }

    return(
      <>
      {isLoading ? (
        <div className="loading-container">
          <RingLoader color="#007bff" loading={isLoading} css={override} size={50} />
        </div>
      ) : (
<CForm className="row g-3" onSubmit={PostData}>
  <CCol md={6}>
    <CFormInput type="email" name="email" id="email" label="Email" />
  </CCol>
  <CCol md={6}>
    <CFormInput type="tel" name="phoneNumber" id="phoneNumber" label="PhoneNumber" />
  </CCol>
  <CCol md={6}>
    <CFormInput type="password" name="password" id="password" label="Password" />
  </CCol>
  <CCol xs={12}>
    <CFormCheck type="checkbox" name="isPersistent" id="isPersistent" label="Check me out"/>
  </CCol>
  <CCol xs={12}>
    <CButton color="primary" type="submit">Sign in</CButton>
  </CCol>
</CForm>
      )}


</>
);}

export default CreateUser




