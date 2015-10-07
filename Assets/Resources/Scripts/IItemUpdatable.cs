using UnityEngine;
using System.Collections;

public interface IItemUpdatable {

	// return true if successfully updated, should be destroyed if not
	bool Update(CarItemHandler car);
}
