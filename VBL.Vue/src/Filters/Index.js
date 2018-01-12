let usDollars = (amount) => amount ? '$' + parseFloat(amount).toFixed(2) : ''
let usPhone = (phone) => phone ? phone.replace(/[^0-9]/g, '').replace(/(\d{3})(\d{3})(\d{4})/, '($1) $2-$3') : ''
let ordinal = (i) => {
  let j = i % 10
  let k = i % 100
  if (j === 1 && k !== 11) {
    return i + 'st'
  }
  if (j === 2 && k !== 12) {
    return i + 'nd'
  }
  if (j === 3 && k !== 13) {
    return i + 'rd'
  }
  return i + 'th'
}
export {usDollars, usPhone, ordinal}
